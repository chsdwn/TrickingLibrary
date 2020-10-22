<template>
  <div>
    <div v-if="tricks">
      <div v-for="trick in tricks" :key="trick.id">
        {{ trick.id }} - {{ trick.name }}
      </div>
    </div>

    <div v-if="submissions">
      <div v-for="submission in submissions" :key="submission.id">
        {{ submission.description }}
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
</template>

<script lang="ts">
import Vue from 'vue';
import { mapState } from 'vuex';

export default Vue.extend({
  computed: {
    ...mapState('submissions', ['submissions']),
    ...mapState('tricks', ['tricks']),
  },
});
</script>
