export const state = () => ({
  tricks: [],
});

export const getters = {
  trickItems: (state) =>
    state.tricks.map((t) => ({
      value: t.id,
      text: t.name,
    })),
};

export const mutations = {
  setTricks(state, { tricks }) {
    state.tricks = tricks;
  },
  reset(state) {
    state = () => ({ tricks: [] });
  },
};

export const actions = {
  createTrick({ state, commit, dispatch }, { form }) {
    return this.$axios.$post(`/tricks`, form);
  },
  async fetchTricks({ commit }) {
    const tricks = await this.$axios.$get(`/tricks`);
    commit('setTricks', { tricks });
  },
};
