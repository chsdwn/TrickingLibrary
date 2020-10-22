<template>
  <v-dialog :value="active" persistent>
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">
          Select Type
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step :complete="step > 2" step="2">
          Upload Video
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step :complete="step > 3" step="3">
          Trick Information
        </v-stepper-step>

        <v-divider></v-divider>

        <v-stepper-step step="4">Review</v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <div class="d-flex flex-column align-center">
            <v-btn class="my-2" @click="setType(uploadType.TRICK)">
              Trick
            </v-btn>
            <v-btn class="my-2" @click="setType(uploadType.SUBMISSION)">
              Submission
            </v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="2">
          <div>
            <v-file-input accept="video/*" @change="handleFile"></v-file-input>
          </div>
        </v-stepper-content>

        <v-stepper-content step="3">
          <div>
            <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
            <v-btn @click="saveTrick">Save</v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="4">
          <div>Success</div>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>

    <div class="d-flex justify-center my-4">
      <v-btn @click="toggleActivity">Close</v-btn>
    </div>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapMutations, mapState } from 'vuex';
import { UPLOAD_TYPE } from '@/data/enum';

export default Vue.extend({
  name: 'VideoUpload',

  data() {
    return {
      trickName: '',
    };
  },

  computed: {
    ...mapState('video-upload', ['active', 'step', 'uploadPromise']),
    uploadType: () => UPLOAD_TYPE,
  },

  methods: {
    ...mapMutations('video-upload', ['reset', 'setType', 'toggleActivity']),
    ...mapActions('video-upload', ['createTrick', 'startVideoUpload']),
    async handleFile(file: File) {
      if (!file) return;

      const videoForm = new FormData();
      videoForm.append('video', file);

      this.startVideoUpload({ videoForm });
    },
    async saveTrick() {
      if (!this.uploadPromise) return;
      const video = await this.uploadPromise;

      await this.createTrick({ trick: { name: this.trickName, video } });
      this.trickName = '';
      this.reset();
    },
  },
});
</script>
