const state = () => {};

export const mutations = {
  reset(state) {
    state = () => ({});
  },
};

export const actions = {
  async nuxtServerInit({ dispatch }) {
    dispatch('tricks/fetchTricks');
  },
};
