<template>
  <div>
    <div v-if="tricks">
      <div v-for="trick in tricks" :key="trick.id">
        {{ trick.name }}
        <div>
          <video
            :src="`http://localhost:5000/api/videos/${trick.video}`"
            width="400"
            controls
          />
        </div>
      </div>
    </div>

    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">Upload</v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step :complete="step > 2" step="2">
          Trick Information
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step step="3">Confirmation</v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <div>
            <v-file-input accept="video/*" @change="handleFile"></v-file-input>
          </div>
        </v-stepper-content>

        <v-stepper-content step="2">
          <div>
            <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
            <v-btn @click="saveTrick">Save</v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="3">
          <div>Success</div>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapMutations, mapState } from 'vuex';
import { State } from '@/store';

export default Vue.extend({
  components: {},

  data() {
    return {
      trickName: '',
      step: 1,
    };
  },

  computed: {
    ...mapState('tricks', ['tricks']),
    ...mapState('videos', ['uploadPromise']),
  },

  methods: {
    ...mapMutations('videos', {
      resetVideos: 'reset',
    }),
    ...mapActions('tricks', ['createTrick']),
    ...mapActions('videos', ['startVideoUpload']),
    async handleFile(file: File) {
      if (!file) return;

      const videoForm = new FormData();
      videoForm.append('video', file);

      this.startVideoUpload({ videoForm });
      ++this.step;
    },
    async saveTrick() {
      if (!this.uploadPromise) return;
      const video = await this.uploadPromise;

      await this.createTrick({ trick: { name: this.trickName, video } });
      this.trickName = '';
      ++this.step;
      this.resetVideos();
    },
  },
});
</script>
