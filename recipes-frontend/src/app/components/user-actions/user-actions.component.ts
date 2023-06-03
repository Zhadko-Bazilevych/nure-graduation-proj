import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { recipe } from 'src/app/models/filter.model';
import { UserRecipeService } from 'src/app/services/userRecipe.service';

@Component({
  selector: 'app-user-actions',
  templateUrl: './user-actions.component.html',
  styleUrls: ['./user-actions.component.scss']
})
export class UserActionsComponent implements OnInit {
  
  constructor(private route: ActivatedRoute, private router: Router, private userRecipeService: UserRecipeService) { }
  
  active: number = 1;

  Recipes: recipe[] = [];


  ngOnInit(): void {
    this.getData(this.active)
  }

  getData(id: number){  
    this.active = id;
    if(id!=4)
    {
      this.userRecipeService.getUserList(id).toPromise().then(
        response => {
          if(response.code == 200)
          {
            this.Recipes = response.recipes
          }
        }
      )
    }
    else
    {
      
    }
  }

  recipe(id: number){
    this.router.navigate([`/recipe`, id]);
  }

  editRecipe(id: number){
    console.log("I'll edit ", id )
  }

  deleteRecipe(id: number){
    console.log("I'll delete ", id )
  }

}
