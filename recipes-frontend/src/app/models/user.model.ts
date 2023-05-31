import { BaseResponse } from "./baseResponse";

export interface User extends BaseResponse{
    mail: string,
    name: string,
    accessToken: string,
    refreshToken: string
}