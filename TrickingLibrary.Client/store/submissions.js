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
  createSubmission({ state, commit, dispatch }, { form }) {
    return this.$axios.$post('/submissions', form);
  },
  async fetchSubmissionsForTrick({ commit }, { trickId }) {
    const submissions = await this.$axios.$get(
      `/tricks/${trickId}/submissions`,
    );
    commit('setSubmissions', { submissions });
  },
};
