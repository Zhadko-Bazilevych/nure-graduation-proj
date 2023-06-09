import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/recipe.service';
import { RecipeInfo, RecipeResponse } from 'src/app/models/recipe.model';
import { faStar, faBook, faBookmark, faBowlFood, faTriangleExclamation, faWheatAwn, faDroplet, faBacon, faUtensils, faXmark } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkOut, faStar as faStarOutline, faClock} from '@fortawesome/free-regular-svg-icons';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { UserRecipeService } from 'src/app/services/userRecipe.service';
import { AuthorPage } from 'src/app/models/user.model';
import { recipe } from 'src/app/models/filter.model';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { GlobalDataService } from 'src/app/services/globalData.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  BaseUrl: string = "https://localhost:7137/"

  constructor(private route: ActivatedRoute, private router: Router, private userRecipeService: UserRecipeService, private sanitizer: DomSanitizer, private fb: FormBuilder, private global: GlobalDataService) { }
  private routeSub: Subscription;
  recipesIcon = faBook;
  subsIcon = faStar;

  Author: AuthorPage;
  Recipes: recipe[];
  isConfiguring: boolean = false;
  
  isImageChanging: boolean = false;
  image: FormData = new FormData();
  oldimage: string | null;

  xIcon = faXmark;

  profileForm: FormGroup;
  get name() { return this.profileForm.get('name') }
  get isPublicMail() { return this.profileForm.get('isPublicMail') }
  get description() { return this.profileForm.get('description') }

  ngOnInit(): void {
    this.routeSub = this.route.params
    .pipe(
      switchMap((params: Params) => {
        const userId = params['id'];
        return userId
      })
    )
    .subscribe((userData) => {
      this.UserInfo(userData)
      this.UserRecipes(userData);
    });
  }

  UserInfo(id: any) {
    const response = this.userRecipeService.getAuthorData(id).toPromise().then(response => {
      if(response.code == 200) {
        this.Author = response.author
        if(this.Author.image != null) {
          this.Author.image = this.BaseUrl+this.Author.image
          this.oldimage = this.Author.image;
        }
        this.profileForm = new FormGroup({
          name: new FormControl(this.Author.name),
          isPublicMail: new FormControl(this.Author.isPublicMail),
          description: new FormControl(this.Author.description),
        });
      }
    }).catch(er => {
      this.router.navigate(['']);
    });
  }

  UserRecipes(id: any){
    this.userRecipeService.getUserList(3, id).toPromise().then(
      response => {
        if(response.code == 200){
            this.Recipes = response.recipes;
        }
      }
    )
  }

  changeSub(){
    this.userRecipeService.changeSubscribe(this.Author.id).toPromise().then(response => {
      if (response.code == 200) {
          this.Author.isSubscribed = !this.Author.isSubscribed;
          if(this.Author.isSubscribed)
          this.Author.amountOfSubscribers = this.Author.amountOfSubscribers + 1;
          else
          this.Author.amountOfSubscribers = this.Author.amountOfSubscribers - 1;
      }
    })    
  }

  recipe(id: number){
    this.router.navigate([`/recipe`, id]);
  }

  insertedPhoto(event: any){
    if (event.target.files && event.target.files[0]) {
      this.image = new FormData()
      this.image.append('image', event.target.files[0]);
      this.Author.image = URL.createObjectURL(event.target.files[0]);
      this.isImageChanging = true;
    }
  }

  deletedPhoto(){
    this.image = new FormData()
    this.Author.image = null;
    this.isImageChanging = true;
  }

  update(){
    this.image.append('isImageChanging',this.isImageChanging.toString())
    this.image.append('authorId', this.Author.id.toString())
    if(this.name?.value != null) { this.image.append('name', this.name.value) }
    if(this.description?.value != null) { this.image.append('description', this.description.value) }
    if(this.isPublicMail?.value != null) { this.image.append('isPublicMail', this.isPublicMail.value) }

    this.userRecipeService.editUser(this.image).toPromise().then(
        response => {
          if(response.code == 200) {
            let curName = ((this.name?.value == null || this.name?.value == '') ? this.Author.name : this.name?.value);
            this.Author.name = curName;
            this.global.name = curName;
            this.Author.description = this.description?.value;
            this.Author.isPublicMail = this.isPublicMail?.value;
            this.isConfiguring = !this.isConfiguring;
            this.oldimage = this.Author.image;
          }
        }
      )
  }

  cancel(){
    this.name?.setValue(this.Author.name);
    this.isPublicMail?.setValue(this.Author.isPublicMail);
    this.description?.setValue(this.Author.description);
    this.isConfiguring = !this.isConfiguring;
    this.Author.image = this.oldimage
  }
}