const state = () => {};

export const mutations = {
  reset(state) {
    state = () => ({});
  },
};

export const actions = {
  async nuxtServerInit({ commit, dispatch }) {
    await dispatch('submissions/fetchSubmissions');
    await dispatch('tricks/fetchTricks');
  },
};
