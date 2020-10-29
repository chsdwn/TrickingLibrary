using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace TrickingLibrary.Api.BackgroundServices.VideoEditing
{
    public class VideoManager
    {
        public const string CONVERTED_PREFIX = "c";
        public const string TEMP_PREFIX = "temp_";

        private readonly IWebHostEnvironment _env;

        public VideoManager(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void DeleteTemporaryVideo(string fileName)
        {
            var path = TemporarySavePath(fileName);
            File.Delete(path);
        }

        public bool Temporary(string fileName) => fileName.StartsWith(TEMP_PREFIX);

        public bool TemporaryVideoExists(string fileName)
        {
            var path = TemporarySavePath(fileName);
            return File.Exists(path);
        }

        public string DevVideoPath(string fileName) =>
            !_env.IsProduction() ? null : Path.Combine(WorkingDirectory, fileName);

        public string GenerateConvertedFileName() => $"{CONVERTED_PREFIX}{DateTime.UtcNow.Ticks}.mp4";

        public string TemporarySavePath(string fileName) =>
            Path.Combine(WorkingDirectory, fileName);

        public string WorkingDirectory => _env.WebRootPath;

        public async Task<string> SaveTemporaryVideo(IFormFile video)
        {
            var fileName = string.Concat(TEMP_PREFIX, DateTime.UtcNow.Ticks, Path.GetExtension(video.FileName));
            var savePath = TemporarySavePath(fileName);

            await using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await video.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}