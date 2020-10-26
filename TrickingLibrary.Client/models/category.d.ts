import { ITrick } from './tricks';

export interface ICategory {
  id: string;
  name: string;
  description: string;
  tricks: ITrick[];
}
