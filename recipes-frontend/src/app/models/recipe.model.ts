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
    name: string | null,
    amount: number | null,
    measurementId: number | null,
    measurement: string | null
}

export interface CollectedRecipeStep{
    id: number | null,
    stepNumber: number | null,
    title: string | null,
    description: string | null,
    image: string | null,
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

export interface RecipeUpdateData{
    id : number,
    name : string | null,
    description : string | null,
    difficulty : number | null,
    requiredTime : number | null,
    servings : number | null,
    caloricValue : number | null,
    proteins : number | null,
    fats : number | null,
    carbohydrates : number | null,
    video : string | null,
    foodType : number | null,
    dishType : number | null,
    isPublished : boolean,
    preparationTips : string[] | null,
    additionalTips : string[] | null,
    menuTypes : number[] | null,
    ingredients : CollectedIngredient[] | null,
    steps : CollectedRecipeStep[]  | null,
    stepsImages : number[] | null
    imagesData : Blob[] | null
}

export interface CommentItem {
    id : number,
    content : string,
    image : string | null,
    userId : number,
    userName : string,
    userImage: string | null
    dateCreated: Date,
    isAuthor : boolean,
    countReplies : number,
    replies: CommentItem[] | null
}

export interface CommentsResponse extends BaseResponse {
    comments: CommentItem[]
}

export interface CreateCommentResponse extends BaseResponse {
    id: number
    image: string | null
}