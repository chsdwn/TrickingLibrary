<template>
  <div class="d-flex justify-center align-start">
    <div v-if="submissions">
      <div v-for="x in 1" :key="x">
        <div v-for="submission in submissions" :key="submission.id">
          {{ submission.id }} - {{ submission.description }} -
          {{ submission.trickId }}
          <div>
            <video
              :src="`http://localhost:5000/api/videos/${submission.video}`"
              width="400"
              controls
            />
          </div>
        </div>
      </div>
    </div>

    <div class="mx-2 sticky">
      <v-sheet class="pa-3 mt-2">
        <div class="text-h6">{{ trick.name }}</div>
        <v-divider class="my-1" />

        <div class="text-body-2">{{ trick.description }}</div>
        <div class="text-body-2">{{ trick.difficulty }}</div>
        <v-divider class="my-1" />

        <div v-for="rd in relatedData" v-if="rd.data.length > 0">
          <div class="text-subtitle-1">{{ rd.title }}</div>

          <v-chip-group>
            <v-chip
              v-for="c in rd.data"
              :key="rd.idFactory(c)"
              :to="rd.routeFactory(c)"
              small
            >
              {{ c.name }}
            </v-chip>
          </v-chip-group>
        </div>
      </v-sheet>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapState, mapGetters } from 'vuex';

export default Vue.extend({
  head() {
    return {
      title: this.trick.name,
      meta: [
        {
          hid: 'description',
          name: 'description',
          content: this.trick.description,
        },
      ],
    };
  },
  computed: {
    ...mapGetters('tricks', ['trickById']),
    ...mapState('submissions', ['submissions']),
    ...mapState('tricks', ['categories', 'tricks']),
    relatedData() {
      return [
        {
          idFactory: (c) => `category-${c.id}`,
          title: 'Categories',
          data: this.categories.filter(
            (c) => this.trick.categories.indexOf(c.id) >= 0,
          ),
          routeFactory: (c) => '/',
        },
        {
          idFactory: (t) => `trick-${t.id}`,
          title: 'Prerequisites',
          data: this.tricks.filter(
            (t) => this.trick.prerequisites.indexOf(t.id) >= 0,
          ),
          routeFactory: (t) => `/tricks/${t.id}`,
        },
        {
          idFactory: (t) => `trick-${t.id}`,
          title: 'Progressions',
          data: this.tricks.filter(
            (t) => this.trick.progressions.indexOf(t.id) >= 0,
          ),
          routeFactory: (t) => `/tricks/${t.id}`,
        },
      ];
    },
    trick() {
      return this.trickById(this.$route.params.trick);
    },
  },

  async fetch() {
    const trickId = this.$route.params.trick;
    await this.$store.dispatch(
      'submissions/fetchSubmissionsForTrick',
      { trickId },
      { root: true },
    );
  },
});
</script>

<style lang="postcss" scoped>
.sticky {
  position: sticky;
  top: 48px;
}
</style>
