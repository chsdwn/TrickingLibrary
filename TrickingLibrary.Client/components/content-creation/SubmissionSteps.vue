<template>
  <v-stepper v-model="step">
    <v-stepper-header>
      <v-stepper-step :complete="step > 1" step="1">
        Upload Video
      </v-stepper-step>
      <v-divider></v-divider>

      <v-stepper-step :complete="step > 2" step="2">
        Select Trick
      </v-stepper-step>
      <v-divider></v-divider>

      <v-stepper-step :complete="step > 3" step="3">
        Submission
      </v-stepper-step>
      <v-divider></v-divider>

      <v-stepper-step step="4">Review</v-stepper-step>
    </v-stepper-header>

    <v-stepper-items>
      <v-stepper-content step="1">
        <div>
          <v-file-input accept="video/*" @change="handleFile"></v-file-input>
        </div>
      </v-stepper-content>

      <v-stepper-content step="2">
        <div>
          <v-select
            :items="trickItems"
            v-model="form.trickId"
            label="Select Trick"
          ></v-select>
          <v-btn @click="step++">Next</v-btn>
        </div>
      </v-stepper-content>

      <v-stepper-content step="3">
        <div>
          <v-text-field
            label="Description"
            v-model="form.description"
          ></v-text-field>
          <v-btn @click="step++">Next</v-btn>
        </div>
      </v-stepper-content>

      <v-stepper-content step="4">
        <v-btn @click="save">Save</v-btn>
      </v-stepper-content>
    </v-stepper-items>
  </v-stepper>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapGetters, mapMutations, mapState } from 'vuex';
import { ITrick } from '~/models/tricks';

const initState = () => ({
  step: 1,
  form: {
    trickId: '',
    video: '',
    description: '',
  },
});

export default Vue.extend({
  name: 'SubmissionSteps',

  data: initState,

  computed: {
    ...mapGetters('tricks', ['trickItems']),
  },

  methods: {
    ...mapMutations('video-upload', ['hide']),
    ...mapActions('video-upload', ['createSubmission', 'startVideoUpload']),
    async handleFile(file: File) {
      if (!file) return;

      const videoForm = new FormData();
      videoForm.append('video', file);

      this.startVideoUpload({ videoForm });
      ++this.step;
    },
    save() {
      this.createSubmission({ form: this.form });
      this.hide();
    },
  },
});
</script>
