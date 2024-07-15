export interface Todo {
    id: number,
    title: string,
    description: string,
    isCompleted: boolean,
    dueDate: Date,
    priority: number,
    assignedUserId?: number,
}