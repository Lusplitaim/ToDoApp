import { User } from "./user";

export interface Todo {
    id: number,
    title: string,
    description: string,
    isCompleted: boolean,
    dueDate: Date,
    priority: number,
    user: User,
}