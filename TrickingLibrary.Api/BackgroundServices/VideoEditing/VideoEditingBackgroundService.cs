using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrickingLibrary.Data;

namespace TrickingLibrary.Api.BackgroundServices.VideoEditing
{
    public class VideoEditingBackgroundService : BackgroundService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<VideoEditingBackgroundService> _logger;
        private readonly ChannelReader<EditVideoMessage> _channelReader;
        private readonly IServiceProvider _serviceProvider;
        private readonly VideoManager _videoManager;

        public VideoEditingBackgroundService(
            IWebHostEnvironment env,
            ILogger<VideoEditingBackgroundService> logger,
            Channel<EditVideoMessage> channel,
            IServiceProvider serviceProvider,
            VideoManager videoManager)
        {
            _env = env;
            _logger = logger;
            _channelReader = channel.Reader;
            _serviceProvider = serviceProvider;
            _videoManager = videoManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _channelReader.WaitToReadAsync(stoppingToken))
            {
                var message = await _channelReader.ReadAsync(stoppingToken);

                try
                {
                    var inputPath = _videoManager.TemporarySavePath(message.Input);
                    var outputName = _videoManager.GenerateConvertedFileName();
                    var outputPath = _videoManager.TemporarySavePath(outputName);

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = Path.Combine(_env.ContentRootPath, "ffmpeg", "ffmpeg.exe"),
                        Arguments = $"-y -i {inputPath} -an -vf scale=540x380 {outputPath}",
                        WorkingDirectory = _videoManager.WorkingDirectory,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    using (var process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        process.WaitForExit();
                    }

                    if (_videoManager.TemporaryVideoExists(outputName))
                    {
                        throw new Exception("FFMPEG failed to generate converted video.");
                    }

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        var submission = ctx.Submissions.FirstOrDefault(s => s.Id.Equals(message.SubmissionId));
                        submission.Video = outputName;
                        submission.VideoProcessed = true;

                        await ctx.SaveChangesAsync(stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Video processing failed for {message.Input}.");
                }
                finally
                {
                    _videoManager.DeleteTemporaryVideo(message.Input);
                }
            }
        }
    }
}