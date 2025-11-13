import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICategoryResponse } from '../interfaces/category-response';

@Injectable({
  providedIn: 'root',
})
export class Category {
  private apiUrl = 'http://localhost:5265/api/v1/category';

  constructor(private http: HttpClient) {}

  getCategories(): Observable<ICategoryResponse[]> {
    return this.http.get<ICategoryResponse[]>(this.apiUrl);
  }
}
