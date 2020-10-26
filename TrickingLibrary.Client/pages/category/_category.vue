<template>
  <div class="mt-3 d-flex justify-center align-start">
    <div class="mx-2">
      <v-text-field
        v-model="filter"
        label="Search"
        placeholder="dunk, layup, shoot"
        prepend-inner-icon="mdi-magnify"
        outlined
      />
      <div v-for="trick in filteredTricks" :key="trick.id">
        {{ trick.id }} - {{ trick.name }} - {{ trick.description }}
      </div>
    </div>

    <v-sheet class="pa-3 mx-2 sticky" v-if="category">
      <div class="text-h6">{{ category.name }}</div>
      <v-divider class="my-1" />
      <div class="text-body-2">{{ category.description }}</div>
    </v-sheet>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapGetters } from 'vuex';

import { ICategory } from '@/models/category';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  data: () => ({
    category: {} as ICategory,
    tricks: [] as ITrick[],
    filter: '',
  }),

  head() {
    if (!this.category) return {};
    return {
      title: this.category.name,
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.category.description,
        },
      ],
    };
  },

  computed: {
    ...mapGetters('tricks', ['categoryById']),
    filteredTricks() {
      if (!this.filter) return this.tricks;

      const normalize = this.filter.trim().toLowerCase();
      return this.tricks.filter(
        (t) =>
          t.name.toLowerCase().includes(normalize) ||
          t.description.toLowerCase().includes(normalize),
      );
    },
  },

  async fetch() {
    const categoryId = this.$route.params.category;
    this.category = this.categoryById(categoryId);
    this.tricks = await this.$axios.$get(`/categories/${categoryId}/tricks`);
  },
});
</script>
