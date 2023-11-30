export type LoginResponse = {
  token: string;
};

export type User = {
  nameid : number;
  unique_name:string;
  given_name:string;
  roles: Array<string>
};

export type ToDoTask = {
  id : number;
  taskContent:string;
  createdDate:Date;
  createdBy: number;
  status: number;
};

export type EditTaskResponse = {
  status: boolean;
};

export type DeleteTaskResponse = {
  status: boolean;
};

export type AddNewTaskModel = {
  TaskContent: string;
  CreatedBy: number;
  status: number;
};