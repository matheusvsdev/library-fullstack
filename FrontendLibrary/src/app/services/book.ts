import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBookResponse } from '../interfaces/book-response';
import { IBookRequest } from '../interfaces/book-request';

@Injectable({
  providedIn: 'root',
})
export class Book {
  private apiUrl = 'http://localhost:5265/api/v1/book';

  constructor(private http: HttpClient) {}

  createBook(book: IBookRequest): Observable<IBookResponse> {
    return this.http.post<IBookResponse>(this.apiUrl, book);
  }

  getBooks(title: string = ''): Observable<IBookResponse[]> {
    return this.http.get<IBookResponse[]>(`${this.apiUrl}?title=${title}`);
  }

  updateBook(id: number | string, book: IBookRequest): Observable<IBookResponse> {
    return this.http.put<IBookResponse>(`${this.apiUrl}/${id}`, book);
  }

  deleteBook(id: number | string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
