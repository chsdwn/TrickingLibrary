import { UPLOAD_TYPE } from '../data/enum';

export const state = () => ({
  active: false,
  step: 1,
  type: '',
  uploadPromise: {},
});

export const mutations = {
  toggleActivity(state) {
    state.active = !state.active;
    if (!state.active) state = { uploadPromise: {}, active: false };
  },
  setTask(state, { uploadPromise }) {
    state.uploadPromise = uploadPromise;
    ++state.step;
  },
  setType(state, { type }) {
    state.type = type;
    ++state.step;
  },
  reset(state) {
    state = { uploadPromise: {} };
  },
};

export const actions = {
  async createTrick({ commit, dispatch }, { trick }) {
    await this.$axios.post(`/tricks`, trick);
    await dispatch('tricks/fetchTricks', null, { root: true });
  },
  startVideoUpload({ commit, dispatch }, { videoForm }) {
    const uploadPromise = this.$axios.$post('/videos', videoForm);
    commit('setTask', { uploadPromise });
  },
};
