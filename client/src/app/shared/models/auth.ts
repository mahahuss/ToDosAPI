export type User = {
  nameid: number;
  unique_name: string;
  given_name: string;
  roles: string[];
};

export type UserProfile = {
  name: string;
  image: FormData;
};

export type UserInfo = {
  id: number;
  username: string;
  fullName: string;
  status: boolean;
  totalTasks: number;
  roles: Map<number, string>;
};

export type ShareTask = {
  taskId: number;
  isEditable: boolean;
  sharedTo: number[];
};

export type Role = {
  Id: number;
  UserType: string;
};
