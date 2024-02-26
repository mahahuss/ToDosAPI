export type ToDoTask = {
  id: number;
  taskContent: string;
  createdDate: Date;
  createdBy: number;
  status: boolean;
  files: UserTaskFile[];
  sharedTasks: SharedTask[];
};

export type SharedTask = {
  id: number;
  taskId: number;
  sharedBy?: number;
  sharedWith: number;
  isEditable: boolean;
  sharedDate: Date;
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

export type UserTask = {
  id: number;
  taskContent: string;
  createdDate: Date;
  createdBy: number;
  status: boolean;
  files: UserTaskFile[];
};

export type ShareTask = {
  taskId: number;
  isEditable: boolean;
  sharedWith: number[];
};

export type UserFile = {
  id: number;
  taskId: number;
  fileName: string;
  file?: File;
  isOld: boolean;
};
