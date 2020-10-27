<template>
  <div>
    <v-row>
      <v-col cols="12" sm="7" lg="6" offset-lg="1" xl="4" offset-xl="3">
        <slot name="content" />
      </v-col>

      <v-col class="d-none d-sm-block" sm="5" lg="4" xl="3">
        <v-sheet class="pa-3 sticky">
          <slot name="item" />
        </v-sheet>
      </v-col>
    </v-row>

    <v-dialog v-model="dialog">
      <template v-slot:activator="{ on }">
        <v-btn
          class="d-sm-none"
          color="darkblue"
          v-on="on"
          fixed
          fab
          bottom
          right
          small
        >
          <v-icon>mdi-help-circle-outline</v-icon>
        </v-btn>
      </template>

      <v-sheet class="pa-2" rounded>
        <slot name="item" />
      </v-sheet>
    </v-dialog>
  </div>
</template>

<script>
import Vue from 'vue';
export default Vue.extend({
  name: 'ItemContentLayout',
  data: () => ({
    dialog: false,
  }),
});
</script>

<style></style>
