<template>
  <v-card>
    <v-card-title>Create Category</v-card-title>

    <v-card-text>
      <v-text-field label="Name" v-model="form.name" />
      <v-text-field label="Description" v-model="form.description" />
    </v-card-text>

    <v-card-actions>
      <v-btn @click="save">Save</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import Vue from 'vue';

const initState = () => ({
  form: {
    name: '',
    description: '',
  },
});

export default Vue.extend({
  name: 'CategoryForm',
  data: initState,
  methods: {
    save() {
      this.$axios.post('/categories', this.form);
    },
  },
});
</script>
