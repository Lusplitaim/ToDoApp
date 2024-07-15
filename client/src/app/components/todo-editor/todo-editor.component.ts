import { Component, inject, Input, OnInit } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { FormControl, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { TodoCreateOrUpdate } from '../../models/todoCreateOrUpdate';
import { TodoService } from '../../services/todo.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Todo } from '../../models/todo';

@Component({
  selector: 'app-todo-editor',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './todo-editor.component.html',
  styleUrl: './todo-editor.component.scss'
})
export class TodoEditorComponent implements OnInit {
  formBuilder = inject(FormBuilder);
  todoService = inject(TodoService);
  activeModal = inject(NgbActiveModal);

  @Input() todo: Todo | undefined;
  editMode = false;

  ngOnInit(): void {
    if (this.todo) {
      this.editMode = true;

      this.todoForm.setValue({
        title: this.todo.title,
        description: this.todo.description,
        dueDate: this.todo.dueDate,
        priority: this.todo.priority
      });
    }
  }

  todoForm = this.formBuilder.group({
    title: new FormControl<string| undefined>(undefined, [Validators.required]),
    description: new FormControl<string | undefined>(undefined, [Validators.required]),
    dueDate: new FormControl<Date | undefined>(undefined, [Validators.required]),
    priority: new FormControl<number>(3, [Validators.required]),
  });

  saveTodo(): void {
    const canSave = this.editMode ? this.todoForm.valid : this.todoForm.dirty;
    if (!canSave) {
      return;
    }

    let model: TodoCreateOrUpdate = {
      title: this.getControlValue(TodoFormKeys.Title),
      description: this.getControlValue(TodoFormKeys.Description),
      dueDate: this.getControlValue(TodoFormKeys.DueDate),
      priority: this.getControlValue(TodoFormKeys.Priority),
    };

    if (this.editMode) {
      this.todoService.editTodo(this.todo!.id, model)
        .subscribe(editedTodo => {
          this.activeModal.close(editedTodo);
        });
    } else {
      this.todoService.createTodo(model)
        .subscribe(createdTodo => {
          this.activeModal.close(createdTodo);
        });
    }
  }

  private getControlValue(key: TodoFormKeys) {
    return this.todoForm.get(this.formControlNames[key])!.value;
  }

  private formControlNames: Record<TodoFormKeys, string> = {
    [TodoFormKeys.Title]: "title",
    [TodoFormKeys.Description]: "description",
    [TodoFormKeys.DueDate]: "dueDate",
    [TodoFormKeys.Priority]: "priority",
  };
}

enum TodoFormKeys {
  Title,
  Description,
  DueDate,
  Priority,
}
