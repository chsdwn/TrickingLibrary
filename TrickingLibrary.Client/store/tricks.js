const initState = () => ({
  tricks: [],
  categories: [],
  difficulties: [],
});

export const state = initState;

export const getters = {
  categoryById: (state) => (id) => state.categories.find((c) => c.id === id),
  categoryItems: (state) =>
    state.categories.map((c) => ({
      value: c.id,
      text: c.name,
    })),
  difficultyById: (state) => (id) =>
    state.difficulties.find((d) => d.id === id),
  difficultyItems: (state) =>
    state.difficulties.map((d) => ({
      value: d.id,
      text: d.name,
    })),
  trickById: (state) => (id) => state.tricks.find((t) => t.id === id),
  trickItems: (state) =>
    state.tricks.map((t) => ({
      value: t.id,
      text: t.name,
    })),
};

export const mutations = {
  setTricks(state, { categories, difficulties, tricks }) {
    state.categories = categories;
    state.difficulties = difficulties;
    state.tricks = tricks;
  },
  reset(state) {
    Object.assign(state, initState);
  },
};

export const actions = {
  createTrick({ state, commit, dispatch }, { form }) {
    return this.$axios.$post(`/tricks`, form);
  },
  async fetchTricks({ commit }) {
    const categories = await this.$axios.$get('/categories');
    const difficulties = await this.$axios.$get('/difficulties');
    const tricks = await this.$axios.$get('/tricks');
    commit('setTricks', { categories, difficulties, tricks });
  },
};
