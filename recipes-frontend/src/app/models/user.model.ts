import { BaseResponse } from "./baseResponse";

export interface User extends BaseResponse{
    mail: string,
    name: string,
    accessToken: string,
    refreshToken: string
}

export interface Author extends BaseResponse{
    id : number
    name : string
    image : string
    amountOfSubscribers : number
    amountOfRecipes : number
}