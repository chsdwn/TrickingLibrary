<template>
  <div>
    <div v-if="tricks">
      <div v-for="trick in tricks" :key="trick.id">
        <v-btn :to="`/tricks/${trick.id}`">
          {{ trick.name }}
        </v-btn>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapState } from 'vuex';

export default Vue.extend({
  computed: {
    ...mapState('tricks', ['tricks']),
  },

  async fetch() {
    await this.$store.dispatch('tricks/fetchTricks');
  },
});
</script>
