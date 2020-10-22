<template>
  <v-dialog :value="active" persistent>
    <template v-slot:activator="{ on }">
      <v-btn depressed @click="toggleActivity">Upload</v-btn>
    </template>

    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">
          Select Type
        </v-stepper-step>
        <v-divider v-if="type === uploadType.TRICK"></v-divider>

        <v-stepper-step
          :complete="step > 2"
          step="2"
          v-if="type === uploadType.TRICK"
        >
          Trick Information
        </v-stepper-step>
        <v-divider></v-divider>

        <v-stepper-step :complete="step > 3" step="3">
          Upload Video
        </v-stepper-step>
        <v-divider></v-divider>

        <v-stepper-step :complete="step > 4" step="4">
          Submission Information
        </v-stepper-step>
        <v-divider></v-divider>

        <v-stepper-step step="5">Review</v-stepper-step>
      </v-stepper-header>

      <v-stepper-items>
        <v-stepper-content step="1">
          <div class="d-flex flex-column align-center">
            <v-btn class="my-2" @click="setType({ type: uploadType.TRICK })">
              Trick
            </v-btn>
            <v-btn
              class="my-2"
              @click="setType({ type: uploadType.SUBMISSION })"
            >
              Submission
            </v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="2">
          <div>
            <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
            <v-btn @click="incrementStep">Save</v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="3">
          <div>
            <v-file-input accept="video/*" @change="handleFile"></v-file-input>
          </div>
        </v-stepper-content>

        <v-stepper-content step="4">
          <div>
            <v-text-field
              label="Description"
              v-model="submission"
            ></v-text-field>
            <v-btn @click="incrementStep">Save Submission</v-btn>
          </div>
        </v-stepper-content>

        <v-stepper-content step="5">
          <v-btn @click="save">Save</v-btn>
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
      submission: '',
      trickName: '',
    };
  },

  computed: {
    ...mapState('video-upload', ['active', 'step', 'type', 'uploadPromise']),
    uploadType: () => UPLOAD_TYPE,
  },

  methods: {
    ...mapMutations('video-upload', [
      'incrementStep',
      'reset',
      'setType',
      'toggleActivity',
    ]),
    ...mapActions('video-upload', ['createTrick', 'startVideoUpload']),
    async handleFile(file: File) {
      if (!file) return;

      const videoForm = new FormData();
      videoForm.append('video', file);

      this.startVideoUpload({ videoForm });
    },
    async save() {
      if (!this.uploadPromise) return;
      const video = await this.uploadPromise;

      await this.createTrick({
        trick: { name: this.trickName },
        submission: { description: this.submission, video, trickId: 1 },
      });
      this.submission = '';
      this.trickName = '';
      this.reset();
    },
  },
});
</script>
