import { ICategory } from './category';

export interface ITrick {
  id: string;
  name: string;
  description: string;
  difficulty: string;
  categories: string[];
  prerequisites: string[];
  progressions: string[];
}
