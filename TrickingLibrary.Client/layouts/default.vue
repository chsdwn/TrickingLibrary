<template>
  <v-app dark>
    <v-app-bar dense app>
      <v-toolbar-title>
        <nuxt-link to="/" class="link"> Tricking Library </nuxt-link>
      </v-toolbar-title>

      <v-spacer></v-spacer>

      <video-upload />
    </v-app-bar>

    <v-main>
      <nuxt />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import VideoUpload from '@/components/VideoUpload.vue';

export default Vue.extend({
  components: { VideoUpload },
});
</script>
