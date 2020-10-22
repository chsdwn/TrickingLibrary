export const state = () => {
  submissions: [];
};

export const mutations = {
  setSubmissions(state, { submissions }) {
    state.submissions = submissions;
  },
  reset() {
    state = () => ({
      submissions: [],
    });
  },
};

export const actions = {
  async fetchSubmissions({ commit }) {
    const submissions = await this.$axios.$get('/submissions');
    commit('setSubmissions', { submissions });
  },
};
