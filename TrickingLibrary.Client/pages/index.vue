<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <v-card>
        <v-card-title class="headline">
          <div v-if="tricks">
            <p v-for="trick in tricks" :key="trick.id">
              {{ trick.name }}
            </p>
          </div>

          <div>
            <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
            <v-btn @click="saveTrick">Save</v-btn>
          </div>

          {{ message }}
          <v-btn @click="reset">Reset Message</v-btn>
          <v-btn @click="resetTricks">Reset Tricks</v-btn>
        </v-card-title>
      </v-card>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from 'vue';
import { mapActions, mapMutations, mapState } from 'vuex';
import { State } from '@/store';

export default Vue.extend({
  components: {},

  data() {
    return {
      trickName: '',
    };
  },

  computed: {
    ...mapState({
      message: (state: any) => state.message,
    }),
    ...mapState('tricks', {
      tricks: (state: any) => state.tricks,
    }),
  },

  methods: {
    ...mapMutations(['reset']),
    ...mapMutations('tricks', {
      resetTricks: 'reset',
    }),
    ...mapActions('tricks', ['createTrick']),
    async saveTrick() {
      await this.createTrick({ name: this.trickName });
      this.trickName = '';
    },
  },
});
</script>
