import { BaseResponse } from "./baseResponse"
import { IdItem } from "./idItem"

export interface RecipeResponse extends BaseResponse{
    recipe: RecipeInfo
}

export interface RecipeInfo{
    id : number,
    userId : number,
    userName : string,
    name : string,
    description : string,
    difficulty : number,
    requiredTime : number,
    servings : number,
    caloricValue : number,
    proteins : number,
    fats : number,
    carbohydrates : number,
    video : string,
    amountOfRates : number | null,
    amountOfFavorites : number | null,
    foodTypeId : number | null,
    foodType : string | null,
    dishTypeId : number | null,
    dishType : string | null,
    rating : number | null,
    creationDate : string,
    isPublished : boolean,
    preparationTips : string[],
    additionalTips : string[],
    menuTypes : string[],
    comments : CollectedComment[],
    ingredients : CollectedIngredient[],
    steps : CollectedRecipeStep[],
    images : string[],
    isFavorite : boolean,
    userRate : number,
}

export interface CollectedIngredient{
    id: number| null,
    ingredientId: number | null
    name: string,
    amount: number,
    measurementId: number | null,
    measurement: string
}

export interface CollectedRecipeStep{
    id: number,
    stepNumber: number | null,
    title: string,
    description: string,
    image: string,
}

export interface CollectedComment{
    id: number,
    content: string,
    image: string,
    userId: number,
    userName: string,
    dateCreated: Date,
    isAuthor: boolean
}

export interface idResponse extends BaseResponse{
    id: number,
}


export interface RecipeUpdateInfo{
    id : number,
    userId : number,
    userName : string,
    name : string,
    description : string,
    difficulty : number,
    requiredTime : number,
    servings : number,
    caloricValue : number,
    proteins : number,
    fats : number,
    carbohydrates : number,
    video : string,
    foodType : IdItem,
    dishType : IdItem,
    rating : number | null,
    creationDate : string,
    isPublished : boolean,
    preparationTips : IdItem[] | null,
    additionalTips : IdItem[] | null,
    menuTypes : IdItem[] | null,
    ingredients : CollectedIngredient[],
    steps : CollectedRecipeStep[],
    images : IdItem[]
}

export interface RecipeUpdateResponse extends BaseResponse{
    recipe: RecipeUpdateInfo
}