import { BaseResponse } from "./baseResponse";
import { IdItem } from "./idItem";

export interface IngredientsResponse extends BaseResponse{
 ingredients: IdItem[]
}

export interface filterSelecDataResponse extends BaseResponse{
    dishTypes: IdItem[]
    menuTypes: IdItem[]
    foodTypes: IdItem[]
   }

export interface filterResponse extends BaseResponse{
    recipeId: number
    name: string
    image: string
    requiredTime: number
    difficulty: number
    author: string
    authorId: number
    description: string
}
