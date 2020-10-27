<template>
  <ItemContentLayout>
    <template v-slot:content>
      <TrickList :tricks="tricks" class="mx-2" />
    </template>

    <template v-slot:item>
      <div v-if="category">
        <div class="text-h6">{{ category.name }}</div>
        <v-divider class="my-1" />
        <div class="text-body-2">{{ category.description }}</div>
      </div>
    </template>
  </ItemContentLayout>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapGetters } from 'vuex';

import ItemContentLayout from '@/components/ItemContentLayout.vue';
import TrickList from '@/components/TrickList.vue';

import { ICategory } from '@/models/category';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  components: { ItemContentLayout, TrickList },

  data: () => ({
    category: {} as ICategory,
    tricks: [] as ITrick[],
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
  },

  async fetch() {
    const categoryId = this.$route.params.category;
    this.category = this.categoryById(categoryId);
    this.tricks = await this.$axios.$get(`/categories/${categoryId}/tricks`);
  },
});
</script>
