import { Store } from 'vuex';
import axios from 'axios';

import { ITrick } from '@/models/tricks';

const API_URL = 'http://localhost:5000/api';

export const state = {
  tricks: [] as ITrick[],
};

export type State = typeof state;

export const mutations = {
  setTricks(state: State, tricks: ITrick[]) {
    state.tricks = tricks;
  },
  reset(state: State) {
    state = { tricks: [] as ITrick[] };
  },
};

export const actions = {
  async fetchTricks({ commit }: Store<any>) {
    const tricks = (await axios.get<ITrick[]>(`${API_URL}/tricks`)).data;
    commit('setTricks', tricks);
  },
  async createTrick({ commit, dispatch }: Store<any>, trick: ITrick) {
    await axios.post(`${API_URL}/tricks`, trick);
    await dispatch('fetchTricks');
  },
};
