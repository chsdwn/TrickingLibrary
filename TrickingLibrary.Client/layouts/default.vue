<template>
  <v-app dark>
    <v-app-bar dense app>
      <v-toolbar-title>Tricking Library</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn depressed @click="toggleActivity">Upload</v-btn>
    </v-app-bar>

    <video-upload />

    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapMutations } from 'vuex';

import VideoUpload from '@/components/VideoUpload.vue';

export default Vue.extend({
  components: { VideoUpload },

  methods: mapMutations('videos', ['toggleActivity']),
});
</script>
