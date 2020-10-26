import { ITrick } from './tricks';

export interface IDifficulty {
  id: string;
  name: string;
  description: string;
  tricks: ITrick[];
}
