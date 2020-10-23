<template>
  <v-dialog :value="active" persistent>
    <template v-slot:activator="{ on }">
      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn depressed v-bind="attrs" v-on="on">Create</v-btn>
        </template>

        <v-list>
          <v-list-item
            v-for="(item, index) in menuItems"
            :key="`ccd-menu-${index}`"
            @click="activate({ component: item.component })"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </template>

    <div v-if="component">
      <component :is="component"></component>
    </div>

    <div class="d-flex justify-center my-4">
      <v-btn @click="reset">Close</v-btn>
    </div>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapMutations, mapState } from 'vuex';

import SubmissionSteps from '@/components/content-creation/SubmissionSteps.vue';
import TrickSteps from '@/components/content-creation/TrickSteps.vue';

export default Vue.extend({
  name: 'ContentCreationDialog',

  components: { SubmissionSteps, TrickSteps },

  computed: {
    ...mapState('video-upload', ['active', 'component']),
    menuItems() {
      return [
        { component: SubmissionSteps, title: 'Submission' },
        { component: TrickSteps, title: 'Trick' },
      ];
    },
  },

  methods: {
    ...mapMutations('video-upload', ['activate', 'reset']),
  },
});
</script>
