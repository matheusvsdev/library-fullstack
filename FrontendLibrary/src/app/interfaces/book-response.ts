import { ICategoryResponse } from './category-response';

export interface IBookResponse {
  id: number;
  title: string;
  author: string;
  categories: ICategoryResponse[];
}
