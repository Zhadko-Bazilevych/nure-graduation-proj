import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { IngredientsResponse, filter, filterResponse, filterSelecDataResponse, idResponse, patternUpdate, patternsResponse } from '../models/filter.model';
import { BaseResponse } from '../models/baseResponse';

@Injectable({
  providedIn: 'root'
})
export class PatternService {
  BaseURL = "https://localhost:7137/api/Recipe/"

  constructor(private httpClient: HttpClient, private router: Router) { }

//   getIngredients(filter: string): Observable<IngredientsResponse>{
//     let params = new HttpParams();

//     const request = {
//       name: filter
//     };

//     return this.httpClient.post<IngredientsResponse>(this.BaseURL + 'FilterIngredient', request)
//   }

  getPatternList(): Observable<patternsResponse>{
    return this.httpClient.get<patternsResponse>(this.BaseURL + 'PatternList')
  }

  deletePattern(id: number): Observable<BaseResponse>{
    const request = {
        id: id
      };
    return this.httpClient.post<BaseResponse>(this.BaseURL + 'PatternDelete', request )
  }

  updatePattern(pattern: patternUpdate): Observable<idResponse>{
    return this.httpClient.post<idResponse>(this.BaseURL + 'PatternUpdate', pattern )
  }
}
