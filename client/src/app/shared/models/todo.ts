export type ToDoTask = {
  id: number;
  taskContent: string;
  createdDate: Date;
  createdBy: number;
  status: boolean;
  isEditable: boolean;
  sharedBy: string;
  files: UserTaskFile[];
};

export type AddNewTaskModel = {
  taskContent: string;
  createdBy: number;
  status: boolean;
};

export type UserTaskFile = {
  id: number;
  taskId: number;
  fileName: string;
};

export type GetUserTasksResponse = {
  pageNumber: number;
  totalPages: number;
  pageSize: number;
  tasks: ToDoTask[];
};
