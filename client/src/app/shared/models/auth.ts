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

export type Role = {
  id: number;
  userType: string;
};

export type UpdateUser = {
  id: number;
  fullName: string;
  roles: Role[];
};

export type UserToShare = {
  id: number;
  fullName: string;
  status: boolean;
};

export type UserPhoto = {
  fileBase64: string;
};
