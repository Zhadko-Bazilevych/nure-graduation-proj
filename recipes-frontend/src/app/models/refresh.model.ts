import { BaseResponse } from "./baseResponse";

export interface Refresh extends BaseResponse{
    refreshToken: string,
    accessToken: string
}