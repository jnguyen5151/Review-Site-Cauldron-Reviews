
export interface RegisterModel {
  userName: string,
  email: string,
  password: string
}

export interface LoginModel {
  identifier: string,
  password: string
}

export interface UserModel {
  id: string,
  userName: string,
  role: string[],
  displayName: string,
  email: string
}
