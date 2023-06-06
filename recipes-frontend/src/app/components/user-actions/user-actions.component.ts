import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { recipe } from 'src/app/models/filter.model';
import { Author } from 'src/app/models/user.model';
import { RecipeService } from 'src/app/services/recipe.service';
import { UserRecipeService } from 'src/app/services/userRecipe.service';

@Component({
  selector: 'app-user-actions',
  templateUrl: './user-actions.component.html',
  styleUrls: ['./user-actions.component.scss']
})
export class UserActionsComponent implements OnInit {
  
  constructor(private route: ActivatedRoute, private router: Router, private userRecipeService: UserRecipeService, private recipeService: RecipeService) { }
  
  active: number = 1;

  Recipes: recipe[] = [];
  Authors: Author[] = [];


  ngOnInit(): void {
    this.getData(this.active)
  }

  getData(id: number){  
    this.active = id;
    if(id!=4)
    {
      this.userRecipeService.getUserList(id).toPromise().then(
        response => {
          if(response.code == 200) {
            console.log(response.recipes)
            this.Recipes = response.recipes
          }
        }
      )
    }
    else
    {
      this.userRecipeService.getAuthorSubscriptionList().toPromise().then(
        response => {
          if(response.code == 200)
          {
            this.Authors = response.authors
            console.log(response.authors, this.Authors)
          }
        }
      )
    }
  }

  recipeInfo(id: number){
    this.router.navigate([`/recipe`, id]);
  }

  editRecipe(id: number){
    this.router.navigate(['edit/' + id])
  }

  deleteRecipe(id: number){
    this.recipeService.deleteRecipe(id).then(
      response => {
        if(response.code == 200){
          this.Recipes.splice(this.Recipes.findIndex(x => x.recipeId == id), 1);
        }
      }
    )
  }

  author(id: number){
    this.router.navigate([`/user`, id]);
  }

  deleteAuthor(id: number){
    this.userRecipeService.changeSubscribe(id).toPromise().then(
      response => {
        if(response.code == 200){
          this.Authors = this.Authors.filter(a=>a.id != id);
        }
      }
    );
  }

  createRecipe() {
    this.recipeService.createEmpty().then(
      response => {
        if(response.code == 200){
          this.router.navigate(['edit/' + response.id])
        }
      }
    )
  }
}
