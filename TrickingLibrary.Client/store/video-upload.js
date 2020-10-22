import { UPLOAD_TYPE } from '../data/enum';

export const state = () => ({
  active: false,
  step: 1,
  type: '',
  uploadPromise: {},
});

export const mutations = {
  incrementStep(state) {
    ++state.step;
  },
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
    if (type === UPLOAD_TYPE.TRICK) ++state.step;
    else if (type === UPLOAD_TYPE.SUBMISSION) state.step += 2;
  },
  reset(state) {
    state = () => ({ active: false, step: 1, type: '', uploadPromise: {} });
  },
};

export const actions = {
  async createTrick({ state, commit, dispatch }, { trick, submission }) {
    if (state.type === UPLOAD_TYPE.TRICK) {
      const createdTrick = await this.$axios.post(`/tricks`, trick);
      submission.trickId = createdTrick.id;
    }

    await this.$axios.$post('/submissions', submission);

    await dispatch('submissions/fetchSubmissions', null, { root: true });
    await dispatch('tricks/fetchTricks', null, { root: true });
  },
  startVideoUpload({ commit, dispatch }, { videoForm }) {
    const uploadPromise = this.$axios.$post('/videos', videoForm);
    commit('setTask', { uploadPromise });
  },
};
