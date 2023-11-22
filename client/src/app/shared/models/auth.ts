export type LoginResponse = {
  token: string;
};

export type User = {
  nameid : number;
  unique_name:string;
  given_name:string;
  roles: Array<string>
};

export type toDoTask = {
  id : number;
  taskContent:string;
  createdDate:Date;
  createdBy: number;
  status: number;
};