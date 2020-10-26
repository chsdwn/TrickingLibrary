<template>
  <div class="mt-3 d-flex justify-center align-start">
    <TrickList :tricks="tricks" class="mx-2" />

    <v-sheet class="pa-3 mx-2 sticky" v-if="difficulty">
      <div class="text-h6">{{ difficulty.name }}</div>
      <v-divider class="my-1" />
      <div class="text-body-2">{{ difficulty.description }}</div>
    </v-sheet>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapGetters } from 'vuex';

import TrickList from '@/components/TrickList.vue';

import { IDifficulty } from '@/models/difficulty';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  data: () => ({
    difficulty: {} as IDifficulty,
    tricks: [] as ITrick[],
  }),

  head() {
    if (!this.difficulty) return {};
    return {
      title: this.difficulty.name,
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.difficulty.description,
        },
      ],
    };
  },

  components: {
    TrickList,
  },

  computed: {
    ...mapGetters('tricks', ['difficultyById']),
  },

  async fetch() {
    const difficultyBId = this.$route.params.difficulty;
    this.difficulty = this.difficultyById(difficultyBId);
    console.log(this.difficulty);
    this.tricks = await this.$axios.$get(
      `/difficulties/${difficultyBId}/tricks`,
    );
  },
});
</script>
