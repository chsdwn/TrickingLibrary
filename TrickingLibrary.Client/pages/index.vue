<template>
  <div>
    <div v-if="tricks">
      <div v-for="trick in tricks" :key="trick.id">
        <v-btn :to="`/trick/${trick.id}`" class="my-1">
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
});
</script>
