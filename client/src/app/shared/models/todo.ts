export type ToDoTask = {
  id: number;
  taskContent: string;
  createdDate: Date;
  createdBy: number;
  status: boolean;
};

export type AddNewTaskModel = {
  taskContent: string;
  createdBy: number;
  status: boolean;
};
