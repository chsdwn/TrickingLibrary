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

    <div class="mx-2 sticky">Trick: {{ $route.params.trick }}</div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapState } from 'vuex';

export default Vue.extend({
  computed: mapState('submissions', ['submissions']),

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
