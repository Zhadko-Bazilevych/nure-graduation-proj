import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { faBacon, faBowlFood, faClock, faDroplet, faStar, faTriangleExclamation, faUtensils, faWheatAwn } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkOut } from '@fortawesome/free-solid-svg-icons';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { IdItem } from 'src/app/models/idItem';
import { RecipeInfo } from 'src/app/models/recipe.model';
import { FilterService } from 'src/app/services/filter.service';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-recipe-update',
  templateUrl: './recipe-update.component.html',
  styleUrls: ['./recipe-update.component.scss']
})
export class RecipeUpdateComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private recipeService: RecipeService, private filterService: FilterService, private sanitizer: DomSanitizer) { }

  BaseUrl: string = "https://localhost:7137/"

  starIcon = faStar;
  // outlineStar = faStarOutline;
  // favIcon = faBookmark;
  favIconOut = faBookmarkOut;
  clockIcon = faClock;
  portionIcon = faBowlFood;
  difficultyIcon = faTriangleExclamation;
  carboIcon = faWheatAwn;
  fatsIcon = faDroplet;
  proteinsIcon = faBacon;
  caloricIcon = faUtensils;

  sanitizedVideo: SafeResourceUrl;
  
  Recipe: RecipeInfo;

  formGr: FormGroup;
  isLoadingForm: boolean = true;


  dropdownSettings: IDropdownSettings = {};
  multipleDropdownSettings: IDropdownSettings = {};

  foodList: IdItem[] = [];
  selectedFoodItems: IdItem[] = [];
  dishList: IdItem[] = [];
  selectedDishItems: IdItem[] = [];
  menuList: IdItem[] = [];
  selectedMenuItems: IdItem[] = [];


  ngOnInit(): void {
    let param = this.route.snapshot.paramMap.get('id')
    let id = param == null ? -1 : +param
    this.recipeService.updateRecipeInfo(id).then(
      response => {
        if(response.code == 200){
          this.Recipe = response.recipe;
          this.formGr = new FormGroup({
            name: new FormControl(this.Recipe.name),
            requiredTime: new FormControl(this.Recipe.requiredTime),
            servings: new FormControl(this.Recipe.servings),
            difficulty: new FormControl(this.Recipe.difficulty),
            caloricValue: new FormControl(this.Recipe.caloricValue),
            proteins: new FormControl(this.Recipe.proteins),
            fats: new FormControl(this.Recipe.fats),
            carbohydrates: new FormControl(this.Recipe.carbohydrates),
            foodType: new FormControl({id: this.Recipe.foodTypeId, name: this.Recipe.foodType }),
            dishType: new FormControl({id: this.Recipe.dishTypeId, name: this.Recipe.dishType }),
            menuType: new FormControl(this.Recipe.menuTypeIds?.map((item, index) => ({id: item, name: this.Recipe.menuTypes![index]}))),
          })
          this.isLoadingForm = false;
        }
      }
    )

    this.filterService.getData().toPromise().then(
      response => {
        if(response.code == 200)
        {
          this.foodList = response.foodTypes;
          this.dishList = response.dishTypes;
          this.menuList = response.menuTypes;
        }
      }
    )

    this.dropdownSettings = {
      singleSelection: true,
      closeDropDownOnSelection: true,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: "Пошук...",
      allowRemoteDataSearch: false,
      limitSelection: 10,
      maxHeight: 999,
    };

    this.multipleDropdownSettings = {
      singleSelection: false,
      closeDropDownOnSelection: false,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: "Пошук...",
      allowRemoteDataSearch: false,
      limitSelection: 10,
      maxHeight: 999,
    };
  }

  get name() { return this.formGr.get('name')! }
  get difficulty() { return this.formGr.get('difficulty')! }
  get requiredTime() { return this.formGr.get('requiredTime')! }
  get servings() { return this.formGr.get('servings')! }
  get caloricValue() { return this.formGr.get('caloricValue')! }
  get proteins() { return this.formGr.get('proteins')! }
  get fats() { return this.formGr.get('fats')! }
  get carbohydrates() { return this.formGr.get('carbohydrates')! }

  changeDifficulty(ev: number){
    this.difficulty.setValue(ev)
  }


  getEmbedUrl(){
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.Recipe.video)
  }

  Submit(){
    console.log("And another one")
    for (const field in this.formGr.controls) { // 'field' is a string
      console.log(this.formGr.controls[field].value);
    }
  }
}
