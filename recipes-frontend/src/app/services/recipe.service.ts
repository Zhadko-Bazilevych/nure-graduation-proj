import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeInfo, RecipeResponse, RecipeUpdateInfo, RecipeUpdateResponse } from '../models/recipe.model';
import { BaseResponse } from '../models/baseResponse';
import { idResponse } from '../models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseUrl = "https://localhost:7137/api/Recipe/";

  constructor(private httpClient: HttpClient, private router: Router) {
  }

  async recipeInfo(id: number){
    const route = `${this.baseUrl}${id}`
    return this.httpClient.get<RecipeResponse>(route).toPromise()
  }

  async random(){
    return this.httpClient.get<idResponse>(this.baseUrl + "Random").toPromise()
  }

  async changeRate(id: number, rate: number){
    const route = `${this.baseUrl}Rate`
    const request = {
      newRate: rate,
      recipeId: id
    };

    return this.httpClient.post<BaseResponse>(route, request).toPromise()
  }

  async changeFav(id: number){
    const route = `${this.baseUrl}changeFavorite`
    const request = {
      recipeId: id
    };

    return this.httpClient.post<BaseResponse>(route, request).toPromise()
  }

  async createEmpty(){
    return this.httpClient.post<idResponse>(this.baseUrl + "CreateEmpty", null ).toPromise()
  }

  async deleteRecipe(id: number){
    return this.httpClient.post<BaseResponse>(this.baseUrl + "DeleteRecipe", {recipeId: id}).toPromise()
  }

  async updateRecipeInfo(id: number){

    return this.httpClient.post<RecipeUpdateResponse>(this.baseUrl + "UpdateRecipeInfo", {recipeId: id}).toPromise()
  }

  async addIngredient(ingredientName: string){

    return this.httpClient.post<idResponse>(this.baseUrl + "CreateIngredient", {name: ingredientName}).toPromise()
  }

  async sendSomewhere(something: FormData){
    return this.httpClient.post<idResponse>(this.baseUrl + "UpdateRecipe", something).toPromise()
  }
}
