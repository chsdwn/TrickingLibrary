const API_URL = 'http://localhost:5000/api';

export const state = () => ({
  uploadPromise: {},
});

export const mutations = {
  setTask(state, uploadPromise) {
    state.uploadPromise = uploadPromise;
  },
  reset(state) {
    state = { uploadPromise: {} };
  },
};

export const actions = {
  startVideoUpload({ commit, dispatch }, { videoForm }) {
    const uploadPromise = this.$axios.$post(
      'http://localhost:5000/api/videos',
      videoForm,
    );
    commit('setTask', uploadPromise);
  },
};
