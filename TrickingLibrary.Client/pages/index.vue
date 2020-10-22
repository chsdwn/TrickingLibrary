<template>
  <div>
    <div v-if="tricks">
      <div v-for="trick in tricks" :key="trick.id">
        {{ trick.name }}
        <div>
          <video
            :src="`http://localhost:5000/api/videos/${trick.video}`"
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
  computed: mapState('tricks', ['tricks']),
});
</script>
