import { Store } from 'vuex';
import axios from 'axios';

const initState = {
  message: '',
};

export const state = initState;
export type State = typeof initState;

export const mutations = {
  setMessage(state: State, message: string) {
    state.message = message;
  },
  reset(state: State) {
    Object.assign(state, initState);
  },
};

export const actions = {
  async nuxtServerInit({ commit, dispatch }: Store<any>) {
    const message = (await axios.get<string>('http://localhost:5000/api/home'))
      .data;
    commit('setMessage', message);
    dispatch('tricks/fetchTricks');
  },
};
