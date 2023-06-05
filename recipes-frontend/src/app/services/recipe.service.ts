import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeInfo, RecipeResponse } from '../models/recipe.model';
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

}
