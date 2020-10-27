<template>
  <v-app dark>
    <v-app-bar dense app>
      <nuxt-link to="/" class="title text-h5 text--primary">
        Tricking Library
      </nuxt-link>

      <v-spacer></v-spacer>

      <ContentCreationDialog />
    </v-app-bar>

    <v-main>
      <v-container>
        <nuxt />
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import ContentCreationDialog from '@/components/content-creation/ContentCreationDialog.vue';

export default Vue.extend({
  components: { ContentCreationDialog },
});
</script>

<style lang="postcss" scoped>
.title {
  text-decoration: none;
}
</style>
