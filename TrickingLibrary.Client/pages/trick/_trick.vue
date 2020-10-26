<template>
  <div class="d-flex justify-center align-start">
    <div v-if="submissions" class="mx-2">
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

    <v-sheet class="pa-3 ma-2 sticky">
      <div class="text-h5">
        <span>{{ trick.name }}</span>
        <v-chip class="mb-1 ml-2" :to="`/difficulty/${difficulty.id}`" small>
          {{ difficulty.name }}
        </v-chip>
      </div>
      <v-divider class="my-1" />

      <div class="text-body-2">{{ trick.description }}</div>
      <div class="text-subtitle-1 my-2">{{ difficulty.name }} Difficulty</div>
      <v-divider class="my-1" />

      <div v-for="(rd, i) in relatedData" :key="`related-${i}`">
        <div v-if="rd.data.length > 0"></div>
        <div class="text-subtitle-1">{{ rd.title }}</div>

        <v-chip-group>
          <v-chip
            v-for="d in rd.data"
            :key="rd.idFactory(d)"
            :to="rd.routeFactory(d)"
            small
          >
            {{ d.name }}
          </v-chip>
        </v-chip-group>
      </div>
    </v-sheet>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapState, mapGetters } from 'vuex';

import { ICategory } from '@/models/category';
import { IDifficulty } from '@/models/difficulty';
import { ITrick } from '@/models/tricks';

export default Vue.extend({
  data: () => ({
    difficulty: {} as IDifficulty,
    trick: {} as ITrick,
  }),

  computed: {
    ...mapGetters('tricks', ['difficultyById', 'trickById']),
    ...mapState('submissions', ['submissions']),
    ...mapState('tricks', ['categories', 'tricks']),
    relatedData() {
      return [
        {
          idFactory: (c: ICategory) => `category-${c.id}`,
          title: 'Categories',
          // data: (this.categories as ICategory[]).filter(
          //   (c) => this.trick.categories.indexOf(c.id) >= 0,
          // ),
          data: (this.categories as ICategory[]).filter(
            (c) => this.trick.categories.indexOf(c.id) >= 0,
          ),
          routeFactory: (c: ICategory) => `/category/${c.id}`,
        },
        {
          idFactory: (t: ITrick) => `prerequisites-${t.id}`,
          title: 'Prerequisites',
          data: (this.tricks as ITrick[]).filter(
            (t) => this.trick.prerequisites.indexOf(t.id) >= 0,
          ),
          routeFactory: (t: ITrick) => `/trick/${t.id}`,
        },
        {
          idFactory: (t: ITrick) => `progressions-${t.id}`,
          title: 'Progressions',
          data: (this.tricks as ITrick[]).filter(
            (t) => this.trick.progressions.indexOf(t.id) >= 0,
          ),
          routeFactory: (t: ITrick) => `/trick/${t.id}`,
        },
      ];
    },
  },

  head() {
    if (!this.trick) return {};
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

  async fetch() {
    const trickId = this.$route.params.trick;
    this.trick = this.trickById(trickId);
    this.difficulty = this.difficultyById(this.trick.difficulty);
    await this.$store.dispatch(
      'submissions/fetchSubmissionsForTrick',
      { trickId },
      { root: true },
    );
  },
});
</script>
