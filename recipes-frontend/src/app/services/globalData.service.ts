import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { User } from '../models/user.model';

@Injectable()
export class GlobalDataService implements OnInit{

    id: number | null;
    name: string | null;
    mail: string | null;
    photo: string | null;
    isAuth: boolean = false;
    isLoading: boolean = true;

    constructor(private httpClient: HttpClient) {}

    ngOnInit(): void {
        this.getUserData();
    }
  
    async getUserData() {
        await this.httpClient.get<User>('https://localhost:7137/api/OAuth/GetUserData').toPromise().then(
            response => {
                if(response.code == 200){
                    this.id = response.id!;
                    this.mail = response.mail;
                    this.name = response.name;
                    this.photo = response.photo;
                    this.isAuth = true;
                    
                }
            }
        )
        .finally(
            () => { this.isLoading = false;}
        )
    };
}