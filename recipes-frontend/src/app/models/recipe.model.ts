import { BaseResponse } from "./baseResponse"

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
    amountOfServings : number | null,
    amountOfFavorites : number | null,
    foodTypeId : number,
    foodType : string,
    dishTypeId : number,
    dishType : string,
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
    name: string,
    amount: number,
    measurement: string
}

export interface CollectedRecipeStep{
    stepNumber: number,
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