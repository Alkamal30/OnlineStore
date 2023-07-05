export interface IUserAuthorizationResponseModel {
    login: string;
    password: string;
    role: number;
    jwtToken: string;
}