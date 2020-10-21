import { Store } from 'vuex';

const API_URL = 'http://localhost:5000/api';

export const state = () => ({
  uploadPromise: {} as Promise<string>,
});

export type State = typeof state;

export const mutations = {
  setTask(state: State, uploadPromise: Promise<string>) {
    state.uploadPromise = uploadPromise;
  },
  reset(state: State) {
    state = { uploadPromise: {} as Promise<string> };
  },
};

export const actions = {
  startVideoUpload({ commit, dispatch }: Store<any>, videoForm: FormData) {
    const uploadPromise = this.$axios.$post<string>(
      'http://localhost:5000/api/videos',
      videoForm,
    ) as Promise<string>;
    commit('setTask', uploadPromise);
  },
};
