import { CustomQueryParams } from "./customQueryParams";

export interface TodoFilters extends CustomQueryParams {
    isCompleted?: boolean,
    priorityLevels: number[],
}