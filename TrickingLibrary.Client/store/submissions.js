const initState = () => ({
  submissions: [],
});

export const state = initState;

export const mutations = {
  setSubmissions(state, { submissions }) {
    state.submissions = submissions;
  },
  reset() {
    Object.assign(state, initState);
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
