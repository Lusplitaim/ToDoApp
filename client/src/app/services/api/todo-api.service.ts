import { inject, Injectable } from '@angular/core';
import { BaseApi } from './base-api';
import { HttpClient, HttpParams } from '@angular/common/http';
import { TodoFilters } from '../../models/todoFilters';
import { Observable } from 'rxjs';
import { Todo } from '../../models/todo';
import { TodoCreateOrUpdate } from '../../models/todoCreateOrUpdate';

@Injectable({
  providedIn: 'root'
})
export class TodoApiService extends BaseApi {
  private todosUrl = this.baseUrl + 'todos';
  private http = inject(HttpClient);

  constructor() {
    super();
  }

  getTodos(filters: TodoFilters | null): Observable<Todo[]> {
    let params = new HttpParams();
    if (filters) {
      params = this.addQueryParams(filters);
    }
    return this.http.get<Todo[]>(this.todosUrl, { params: params });
  }

  createTodo(todo: TodoCreateOrUpdate): Observable<Todo> {
    return this.http.post<Todo>(this.todosUrl, todo);
  }

  editTodo(todoId: number, todo: TodoCreateOrUpdate): Observable<Todo> {
    return this.http.put<Todo>(`${this.todosUrl}/${todoId}`, todo);
  }

  deleteTodo(todoId: number): Observable<void> {
    return this.http.delete<void>(`${this.todosUrl}/${todoId}`);
  }
}
