<template>
  <div class="video-container">
    <div
      class="play-button"
      :class="{ 'play-button_hide': playing }"
      @click="playing = !playing"
    >
      <v-icon size="80">mdi-play</v-icon>
    </div>

    <video
      ref="video"
      :src="`http://localhost:5000/api/videos/${video}`"
      muted
      loop
    />
  </div>
</template>

<script lang="ts">
import Vue, { PropOptions } from 'vue';

export default Vue.extend({
  name: 'VideoPlayer',

  props: {
    video: {
      type: String,
      required: true,
    } as PropOptions<string>,
  },

  data: () => ({
    playing: false,
  }),

  watch: {
    playing: function (value: boolean) {
      const videoRef = this.$refs.video as HTMLVideoElement;

      if (value) videoRef.play();
      else videoRef.pause();
    },
  },
});
</script>

<style lang="postcss" scoped>
.video-container {
  position: relative;
  width: 480px;
  display: flex;
  overflow: hidden;

  .play-button {
    z-index: 2;
    position: absolute;
    background-color: rgba(0, 0, 0, 0.35);
    width: 100%;
    height: 100%;

    display: flex;
    justify-content: center;
    align-items: center;

    &_hide {
      opacity: 0;
    }
  }

  video {
    z-index: 1;
    width: 100%;
  }
}
</style>
