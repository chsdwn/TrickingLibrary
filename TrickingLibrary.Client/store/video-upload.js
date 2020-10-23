export const state = () => ({
  active: false,
  component: null,
  uploadPromise: null,
});

export const mutations = {
  activate(state, { component }) {
    state.active = true;
    state.component = component;
  },
  hide(state) {
    state.active = false;
  },
  setTask(state, { uploadPromise }) {
    state.uploadPromise = uploadPromise;
  },
  reset(state) {
    state = () => ({ active: false, component: null, uploadPromise: null });
  },
};

export const actions = {
  async createSubmission({ state, commit, dispatch }, { form }) {
    if (!state.uploadPromise) return;

    form.video = await state.uploadPromise;
    await dispatch('submissions/createSubmission', { form }, { root: true });
    commit('reset');
  },
  startVideoUpload({ commit, dispatch }, { videoForm }) {
    const uploadPromise = this.$axios.$post('/videos', videoForm);
    commit('setTask', { uploadPromise });
  },
};
