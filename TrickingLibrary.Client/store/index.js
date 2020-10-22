const state = () => {};

export const mutations = {
  reset(state) {
    state = {};
  },
};

export const actions = {
  async nuxtServerInit({ commit, dispatch }) {
    dispatch('tricks/fetchTricks');
  },
};
