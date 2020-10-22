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
  async fetchSubmissionsForTrick({ commit }, { trickId }) {
    const submissions = await this.$axios.$get(
      `/tricks/${trickId}/submissions`,
    );
    commit('setSubmissions', { submissions });
  },
};
