import { Store } from 'vuex';

const state = () => {};

export type State = typeof state;

export const mutations = {
  reset(state: State) {
    state = {};
  },
};

export const actions = {
  async nuxtServerInit({ commit, dispatch }: Store<any>) {
    dispatch('tricks/fetchTricks');
  },
};
