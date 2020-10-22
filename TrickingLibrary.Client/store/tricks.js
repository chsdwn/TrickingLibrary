const API_URL = 'http://localhost:5000/api';

export const state = () => ({
  tricks: [],
});

export const mutations = {
  setTricks(state, tricks) {
    state.tricks = tricks;
  },
  reset(state) {
    state = { tricks: [] };
  },
};

export const actions = {
  async fetchTricks({ commit }) {
    const tricks = await this.$axios.$get(`${API_URL}/tricks`);
    commit('setTricks', tricks);
  },
  async createTrick({ commit, dispatch }, { trick }) {
    await this.$axios.post(`${API_URL}/tricks`, trick);
    await dispatch('fetchTricks');
  },
};
