import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { TodoCreateOrUpdate } from '../models/todoCreateOrUpdate';
import { Todo } from '../models/todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private todosUrl = environment.apiUrl + 'todos';

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.todosUrl);
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
