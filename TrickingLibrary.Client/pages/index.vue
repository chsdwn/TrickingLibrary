<template>
  <div>
    <v-file-input accept="video/*" @change="handleFile"></v-file-input>

    <div v-if="tricks">
      <p v-for="trick in tricks" :key="trick.id">
        {{ trick.name }}
      </p>
    </div>

    <div>
      <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
      <v-btn @click="saveTrick">Save</v-btn>
    </div>

    {{ message }}
    <v-btn @click="reset">Reset Message</v-btn>
    <v-btn @click="resetTricks">Reset Tricks</v-btn>
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
    };
  },

  computed: {
    ...mapState({
      message: (state: any) => state.message,
    }),
    ...mapState('tricks', {
      tricks: (state: any) => state.tricks,
    }),
  },

  methods: {
    ...mapMutations(['reset']),
    ...mapMutations('tricks', {
      resetTricks: 'reset',
    }),
    ...mapActions('tricks', ['createTrick']),
    async saveTrick() {
      await this.createTrick({ name: this.trickName });
      this.trickName = '';
    },
    async handleFile(file: File) {
      if (!file) return;

      const videoForm = new FormData();
      videoForm.append('video', file);

      const result = await this.$axios.post(
        'http://localhost:5000/api/videos',
        videoForm,
      );
    },
  },
});
</script>
