import { Component, OnInit } from '@angular/core';
import { GlobalDataService } from './services/globalData.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent{

  constructor(private globalData: GlobalDataService) { }

  ngOnInit(): void {
    this.globalData.getUserData();
  }

  title = 'recipes-frontend';
}
