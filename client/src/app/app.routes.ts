import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { TodosComponent } from './components/todos/todos.component';

export const routes: Routes = [
    {
        path: "",
        redirectTo: "tasks",
        pathMatch: "full",
    },
    {
        path: "",
        canActivate: [authGuard],
        children: [
            { path: "tasks", component: TodosComponent },
        ],
    },
    { path: "login", component: LoginComponent },
    { path: "sign-up", component: SignUpComponent },
];
