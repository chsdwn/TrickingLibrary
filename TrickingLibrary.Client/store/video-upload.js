const initState = () => ({
  active: false,
  component: null,
  uploadCancelSource: null,
  uploadCompleted: false,
  uploadPromise: null,
});

export const state = initState;

export const mutations = {
  activate(state, { component }) {
    state.active = true;
    state.component = component;
  },
  completeUpload(state) {
    state.uploadCompleted = true;
  },
  hide(state) {
    state.active = false;
  },
  setTask(state, { uploadPromise, source }) {
    state.uploadPromise = uploadPromise;
    state.uploadCancelSource = source;
  },
  reset(state) {
    Object.assign(state, initState);
  },
};

export const actions = {
  async cancelUpload({ state, commit }) {
    if (state.uploadPromise) {
      if (state.uploadCompleted) {
        commit('hide');
        const video = await state.uploadPromise;
        console.log(video);
        await this.$axios.delete('/videos', video);
      } else {
        state.uploadCancelSource.cancel();
      }
    }

    commit('reset');
  },
  async createSubmission({ state, commit, dispatch }, { form }) {
    if (!state.uploadPromise) return;

    form.video = await state.uploadPromise;
    await dispatch('submissions/createSubmission', { form }, { root: true });
    commit('reset');
  },
  startVideoUpload({ commit, dispatch }, { videoForm }) {
    const source = this.$axios.CancelToken.source();
    const uploadPromise = this.$axios
      .post('/videos', videoForm, {
        progress: false,
        cancelToken: source.token,
      })
      .then(({ data }) => {
        commit('completeUpload');
        return data;
      })
      .catch((err) => {
        if (this.$axios.isCancel(err)) {
        }
      });

    commit('setTask', { uploadPromise, source });
  },
};
