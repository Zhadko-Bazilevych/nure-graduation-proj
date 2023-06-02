import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { IngredientsResponse, filterResponse, filterSelecDataResponse } from '../models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class FilterService {
  BaseURL = "https://localhost:7137/api/Recipe/"


  constructor(private httpClient: HttpClient, private router: Router) { }

  getIngredients(filter: string): Observable<IngredientsResponse>{
    const request = {
      name: filter
    };

    return this.httpClient.post<IngredientsResponse>(this.BaseURL + 'FilterIngredient', request)
  }

  getData(){
    return this.httpClient.get<filterSelecDataResponse>(this.BaseURL + 'GetFilterData')
  }

  filter(filterRequest: any)
  {
    return this.httpClient.post<filterResponse>(this.BaseURL + 'Filter', filterRequest)
  }

  getPatterns(){
    return this.httpClient.post<filterResponse>(this.BaseURL + 'Pattern', null)
  }
}
