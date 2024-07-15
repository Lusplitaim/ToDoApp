export interface TodoCreateOrUpdate {
    title: string,
    description: string,
    dueDate: Date,
    priority: number,
    assignedUserId?: number,
}