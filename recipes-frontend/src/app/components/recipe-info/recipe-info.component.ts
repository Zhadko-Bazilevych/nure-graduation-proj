import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/recipe.service';
import { RecipeInfo, RecipeResponse } from 'src/app/models/recipe.model';
import { faStar, faBookmark, faBowlFood, faTriangleExclamation, faWheatAwn, faDroplet, faBacon, faUtensils } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkOut, faStar as faStarOutline, faClock} from '@fortawesome/free-regular-svg-icons';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-recipe-info',
  templateUrl: './recipe-info.component.html',
  styleUrls: ['./recipe-info.component.scss']
})
export class RecipeInfoComponent implements OnInit {
  BaseUrl: string = "https://localhost:7137/"

  constructor(private route: ActivatedRoute, private router: Router, private recipeService: RecipeService, private sanitizer: DomSanitizer) { }
  private routeSub: Subscription;
  isLoadingData: boolean = true;

  Recipe: RecipeInfo
  starIcon = faStar;
  outlineStar = faStarOutline;
  favIcon = faBookmark;
  favIconOut = faBookmarkOut;
  clockIcon = faClock;
  portionIcon = faBowlFood;
  difficultyIcon = faTriangleExclamation;
  carboIcon = faWheatAwn;
  fatsIcon = faDroplet;
  proteinsIcon = faBacon;
  caloricIcon = faUtensils;

  sanitizedVideo: SafeResourceUrl;

  choosingStar: number;

  ngOnInit(): void {
    this.routeSub = this.route.params
    .pipe(
      switchMap((params: Params) => {
        const userId = params['id'];
        return userId
      })
    )
    .subscribe((userData) => {
      let param = this.route.snapshot.paramMap.get('id')
    let id = param == null ? -1 : +param
    this.RecipeInfo(id)
    });
  }

  RecipeInfo(id: number) {
    const response = this.recipeService.recipeInfo(id).then(response => {
      if(response.code == 200) {
        this.Recipe = response.recipe
        this.choosingStar = this.Recipe.userRate
        this.sanitizedVideo = this.getEmbedUrl()
        this.isLoadingData = false;
      }
    }).catch(er => {
      this.router.navigate(['']);
    });
  }

  hoverStar(n: number) {
    this.choosingStar = n;
  }

  unhoverStar() {
    this.choosingStar = this.Recipe.userRate;
  }

  chooseStar(r: number) {
    this.recipeService.changeRate(this.Recipe.id, r).then(response => {
      if (response.code == 200) {
        if (r === this.Recipe.userRate) {
          this.choosingStar = 0;
          this.Recipe.userRate = 0;
          this.Recipe.rating = (this.Recipe.amountOfRates! - 1 != 0 ? ((this.Recipe.rating!) * (this.Recipe.amountOfRates!) - r)/(this.Recipe.amountOfRates! - 1) : 0)
          this.Recipe.amountOfRates = this.Recipe.amountOfRates! - 1;
        }
        else {
          
          if(this.Recipe.userRate != 0){
            this.Recipe.rating = ((this.Recipe.rating??0) * (this.Recipe.amountOfRates??0) + r - this.Recipe.userRate) / (this.Recipe.amountOfRates!)
            this.Recipe.amountOfRates = this.Recipe.amountOfRates!;
          }
          else{
            this.Recipe.rating = ((this.Recipe.rating??0) * (this.Recipe.amountOfRates??0) + r) / ((this.Recipe.amountOfRates??0) + 1)
            this.Recipe.amountOfRates = this.Recipe.amountOfRates! + 1;
          }
          this.choosingStar = r;
          this.Recipe.userRate = r;
          
        }
      }
    })
  }

  changeFav(){
    this.recipeService.changeFav(this.Recipe.id).then(response => {
      if (response.code == 200) {
          this.Recipe.isFavorite = !this.Recipe.isFavorite;
          if(this.Recipe.isFavorite)
          this.Recipe.amountOfFavorites = (this.Recipe.amountOfFavorites??0) + 1;
          else
          this.Recipe.amountOfFavorites!--;
      }
    })    
  }

  getEmbedUrl(){
    return this.sanitizer.bypassSecurityTrustResourceUrl("https://www.youtube.com/embed/" + this.Recipe.video)
  }
}
