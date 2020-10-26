<template>
  <v-stepper v-model="step">
    <v-stepper-header>
      <v-stepper-step :complete="step > 1" step="1">
        Trick Information
      </v-stepper-step>
      <v-divider></v-divider>

      <v-stepper-step step="2">Review</v-stepper-step>
    </v-stepper-header>

    <v-stepper-items>
      <v-stepper-content step="1">
        <div>
          <v-text-field label="Trick Name" v-model="form.name" />
          <v-text-field label="Trick Description" v-model="form.description" />
          <v-select
            :items="difficultyItems"
            v-model="form.difficulty"
            label="Select Difficulty"
          />
          <v-select
            :items="trickItems"
            v-model="form.prerequisites"
            label="Prerequisites"
            multiple
            chips
            small-chips
            deletable-chips
          />
          <v-select
            :items="trickItems"
            v-model="form.progressions"
            label="Progressions"
            multiple
            chips
            small-chips
            deletable-chips
          />
          <v-select
            :items="categoryItems"
            v-model="form.categories"
            label="Category"
            multiple
            chips
            small-chips
            deletable-chips
          />
          <v-btn @click="step++">Next</v-btn>
        </div>
      </v-stepper-content>

      <v-stepper-content step="2">
        <v-btn @click="save">Save</v-btn>
      </v-stepper-content>
    </v-stepper-items>
  </v-stepper>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapGetters, mapMutations, mapState } from 'vuex';

const initState = () => ({
  step: 1,
  form: {
    name: '',
    description: '',
    difficulty: '',
    prerequisites: [],
    progressions: [],
    categories: [],
  },
});

export default Vue.extend({
  name: 'TrickSteps',

  data: initState,

  computed: {
    ...mapGetters('tricks', ['categoryItems', 'difficultyItems', 'trickItems']),
    ...mapState('video-upload', ['active']),
  },

  watch: {
    active: function (newValue) {
      if (!newValue) Object.assign(this.$data, initState());
    },
  },

  methods: {
    ...mapActions('tricks', ['createTrick']),
    ...mapMutations('video-upload', ['reset']),
    async save() {
      await this.createTrick({ form: this.form });
      this.reset();
    },
  },
});
</script>
