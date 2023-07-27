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
  isLoading: boolean = true;

  Recipes: recipe[] = [];
  Authors: Author[] = [];


  ngOnInit(): void {
    let param = this.route.snapshot.paramMap.get('id')
    let id = param == null ? -1 : +param
    if(id > 0 && id < 5) { this.active = id }
    console.log('PARAGM', id)
    this.getData(this.active)
  }

  getData(id: number){  
    this.isLoading = true;
    this.active = id;
    if(id!=4)
    {
      this.userRecipeService.getUserList(id).toPromise().then(
        response => {
          if(response.code == 200) {
            this.Recipes = response.recipes
            this.isLoading = false;
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
            this.isLoading = false;
          }
        }
      )
    }
  }

  recipeInfo(id: number){
    if(this.Recipes.find(f => f.recipeId == id)?.isPublished) {
      this.router.navigate([`/recipe`, id]);
    }
    else{
      this.router.navigate([`/edit`, id]);
    }
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
