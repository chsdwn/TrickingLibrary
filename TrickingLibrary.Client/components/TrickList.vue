<template>
  <div>
    <v-text-field
      v-model="filter"
      label="Search"
      placeholder="easy, medium, hard"
      prepend-inner-icon="mdi-magnify"
      outlined
    />
    <div v-for="trick in filteredTricks" :key="trick.id">
      {{ trick.id }} - {{ trick.name }} - {{ trick.description }}
    </div>
  </div>
</template>

<script lang="ts">
import Vue, { PropOptions } from 'vue';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  name: 'TrickList',

  props: {
    tricks: {
      type: Array,
      required: true,
    } as PropOptions<ITrick[]>,
  },

  data: () => ({
    filter: '',
  }),

  computed: {
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
});
</script>
