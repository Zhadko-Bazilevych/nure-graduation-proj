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
    recipes: recipe[]
}

export interface recipe{
    recipeId: number
    name: string
    image: string
    requiredTime: number | null
    difficulty: number | null
    author: string
    authorId: number
    description: string
    isPublished: boolean
}

export interface filter{
    name: string,
    requiredTimeMin: number,
    requiredTimeMax: number,
    asIngredientPool: boolean,
    sortType: string,
    difficultyMin: number,
    difficultyMax: number,
    dishTypeId: IdItem[],
    foodTypeId: IdItem[],
    menuTypeId: IdItem[],
    ingredientId: IdItem[],
    isDescending: boolean,
} 

export interface filterInt{
    name: string,
    requiredTimeMin: number,
    requiredTimeMax: number,
    asIngredientPool: boolean,
    sortType: string,
    difficultyMin: number,
    difficultyMax: number,
    dishTypeId: number[],
    foodTypeId: number[],
    menuTypeId: number[],
    ingredientId: number[],
    isDescending: boolean,
} 

export interface patternUpdate extends filterInt{
    id: number | null
    patternName: string | null
}

export interface patternsResponse extends BaseResponse{
    items: IdItem[],
    patterns: filter[],
}

export interface idResponse extends BaseResponse{
    id: number
}

export interface MeasurementSelecDataResponse extends BaseResponse{
    measurements: IdItem[]
}
