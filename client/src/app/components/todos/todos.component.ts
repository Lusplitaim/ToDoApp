import { Component, inject, OnInit } from '@angular/core';
import { NgbAccordionModule, NgbCollapseModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from '../../services/toast.service';
import { AccountService } from '../../services/account.service';
import { User } from '../../models/user';
import { Todo } from '../../models/todo';
import { TodoService } from '../../services/todo.service';
import { TodoEditorComponent } from '../todo-editor/todo-editor.component';
import { SharedModule } from '../../shared.module';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { TodoFilters } from '../../models/todoFilters';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [NgbAccordionModule, NgbCollapseModule, SharedModule],
  templateUrl: './todos.component.html',
  styleUrl: './todos.component.scss'
})
export class TodosComponent implements OnInit {
  private todoService = inject(TodoService);
  private toastService = inject(ToastService);
  private accountService = inject(AccountService);
  modal = inject(NgbModal);
  formBuilder = inject(FormBuilder);

  todos: Todo[] = [];
  disabled = false;
  currentUser: User | undefined;
  filtersClosed = true;

  filtersForm = this.formBuilder.group({
    isCompleted: new FormControl<boolean | null>(null),
    priority: new FormControl<number[]>([]),
  });

  ngOnInit(): void {
    this.todoService.getTodos()
      .subscribe(todos => {
        this.todos = todos;
      });

    this.currentUser = this.accountService.getCurrentUser();
  }

  applyFilters(): void {
    const filters: TodoFilters = {
      priorityLevels: this.filtersForm.get("priority")?.value!,
      isCompleted: this.filtersForm.get("isCompleted")?.value!,
    };

    this.todoService.getTodos(filters)
      .subscribe(todos => {
        this.todos = todos;
      });
  }

  resetFilters(): void {
    this.filtersForm.setValue({
      isCompleted: null,
      priority: []
    });
  }

  deleteTodo(todo: Todo): void {
    this.todoService.deleteTodo(todo.id)
      .subscribe(_ => {
        const todoIndex = this.todos.findIndex(el => el.id === todo.id);
        this.todos.splice(todoIndex, 1);
      });
  }

  async openCreationPageModal(): Promise<void> {
    const modalRef = this.modal.open(TodoEditorComponent);
    const result = await modalRef.result;
    if (result) {
      this.todos.push(result);
    }
  }

  openEditModal(todoForEdit: Todo): void {
    const modalRef = this.modal.open(TodoEditorComponent);
    modalRef.componentInstance.todo = todoForEdit;
    modalRef.closed.subscribe(editedTodo => {
      const editedTodoIndex = this.todos.findIndex(el => el.id === editedTodo.id);
      this.todos[editedTodoIndex] = editedTodo;
    });
  }
}
