export type LoginResponse = {
  token: string;
};

export type User = {
  nameid: number;
  unique_name: string;
  given_name: string;
  roles: Array<string>;
};

export type UserProfile = {
  Name: string;
  Image : FormData;
};