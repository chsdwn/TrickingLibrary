export const state = () => ({
  tricks: [],
});

export const mutations = {
  setTricks(state, { tricks }) {
    state.tricks = tricks;
  },
  reset(state) {
    state = { tricks: [] };
  },
};

export const actions = {
  async fetchTricks({ commit }) {
    const tricks = await this.$axios.$get(`/tricks`);
    commit('setTricks', { tricks });
  },
};
