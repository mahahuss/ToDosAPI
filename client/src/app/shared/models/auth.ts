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
  sharedWith: number[];
};

export type Role = {
  id: number;
  userType: string;
};

export type UpdateUser = {
  id: number;
  fullname: string;
  roles: Role[];
};
