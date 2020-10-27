<template>
  <ItemContentLayout>
    <template v-slot:content>
      <TrickList :tricks="tricks" />
    </template>

    <template v-slot:item>
      <div v-if="difficulty">
        <div class="text-h6">{{ difficulty.name }}</div>
        <v-divider class="my-1" />
        <div class="text-body-2">{{ difficulty.description }}</div>
      </div>
    </template>
  </ItemContentLayout>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapGetters } from 'vuex';

import ItemContentLayout from '@/components/ItemContentLayout.vue';
import TrickList from '@/components/TrickList.vue';

import { IDifficulty } from '@/models/difficulty';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  components: {
    ItemContentLayout,
    TrickList,
  },

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
