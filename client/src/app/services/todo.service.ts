import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoCreateOrUpdate } from '../models/todoCreateOrUpdate';
import { Todo } from '../models/todo';
import { TodoFilters } from '../models/todoFilters';
import { TodoApiService } from './api/todo-api.service';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private todoApi = inject(TodoApiService);

  getTodos(filters: TodoFilters | null = null): Observable<Todo[]> {
    return this.todoApi.getTodos(filters);
  }

  createTodo(todo: TodoCreateOrUpdate): Observable<Todo> {
    return this.todoApi.createTodo(todo);
  }

  editTodo(todoId: number, todo: TodoCreateOrUpdate): Observable<Todo> {
    return this.todoApi.editTodo(todoId, todo);
  }

  deleteTodo(todoId: number): Observable<void> {
    return this.todoApi.deleteTodo(todoId);
  }
}
