export type User = {
  nameid: number;
  unique_name: string;
  given_name: string;
  sub: string;
  roles: Array<string>;
};

export type UserProfile = {
  Name: string;
  Image: FormData;
};

export type UserInfo = {
  id: number;
  username: string;
  fullName: string;
  status: string;
  totalTasks: number;
};
