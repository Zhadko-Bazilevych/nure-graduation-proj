import { BaseResponse } from "./baseResponse";

export interface User extends BaseResponse{
    id: number | null
    mail: string,
    name: string,
    accessToken: string,
    refreshToken: string
}

export interface Author{
    id : number
    name : string
    image : string
    amountOfSubscribers : number
    amountOfRecipes : number
}

export interface AuthorPage{
    id : number,
    name : string,
    image : string | null,
    mail : string,
    description : string | null,
    amountOfSubscribers : number,
    amountOfRecipes : number,
    isMe : boolean,
    isSubscribed: boolean,
    isPublicMail: boolean,
}

export interface AuthorListResponse extends BaseResponse{
    authors: Author[]
}

export interface AuthorPageResponse extends BaseResponse{
    author: AuthorPage
}

export interface EditUserRequest{
    authorId: number,
    name: string | null,
    isPublicMail: boolean,
    description: string | null,
}