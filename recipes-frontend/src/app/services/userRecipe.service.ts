import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { IngredientsResponse, MeasurementSelecDataResponse, filterResponse, filterSelecDataResponse } from '../models/filter.model';
import { BaseResponse } from '../models/baseResponse';
import { Author, AuthorListResponse, AuthorPageResponse, EditUserRequest } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserRecipeService {
  BaseURL = "https://localhost:7137/api/UserRecipe/"


  constructor(private httpClient: HttpClient, private router: Router) { }

  // getIngredients(filter: string): Observable<IngredientsResponse>{
  //   const request = {
  //     name: filter
  //   };

  //   return this.httpClient.post<IngredientsResponse>(this.BaseURL + 'FilterIngredient', request)
  // }

  getAuthorSubscriptionList(): Observable<AuthorListResponse>{

    return this.httpClient.get<AuthorListResponse>(this.BaseURL + 'AuthorSubscriptionList')
  }

  changeSubscribe(id: number): Observable<BaseResponse>{

    return this.httpClient.post<BaseResponse>(this.BaseURL + 'ChangeSubscribe', {authorId : id})
  }

  getUserList(type: number, authorId: number | null = null): Observable<filterResponse>{

    return this.httpClient.post<filterResponse>(this.BaseURL + 'GetUserList', {listType : type, authorId: authorId})
  }

  getAuthorData(id: number): Observable<AuthorPageResponse>{

    return this.httpClient.post<AuthorPageResponse>(this.BaseURL + 'AuthorInfo', {authorId : id})
  }

  editUser(request: EditUserRequest): Observable<BaseResponse>{

    return this.httpClient.post<BaseResponse>(this.BaseURL + 'EditUser', request)
  }

  getMeasurementsData(): Observable<MeasurementSelecDataResponse>{
    return this.httpClient.get<MeasurementSelecDataResponse>(this.BaseURL + 'GetMeasurementsData')
  }
}
