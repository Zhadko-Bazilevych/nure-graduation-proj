import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { faBacon, faBowlFood, faClock, faDroplet, faStar, faTriangleExclamation, faUtensils, faWheatAwn, faXmark } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkOut } from '@fortawesome/free-solid-svg-icons';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { IdItem } from 'src/app/models/idItem';
import { RecipeInfo, RecipeUpdateInfo } from 'src/app/models/recipe.model';
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
  xIcon = faXmark;

  sanitizedVideo: SafeResourceUrl;
  
  Recipe: RecipeUpdateInfo;
  RecipeImages: string[] | null

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
          this.RecipeImages = this.Recipe?.images.map(s=>s.name);
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
            foodType: new FormControl([this.Recipe.foodType]),
            dishType: new FormControl([this.Recipe.dishType]),
            menuType: new FormControl(this.Recipe.menuTypes),
            description: new FormControl(this.Recipe.description),
            preparationTips: new FormArray(
              (this.Recipe.preparationTips != null && this.Recipe.preparationTips.length != 0 ? this.Recipe.preparationTips.map(m => new FormControl(m.name)) : [])),
            steps: new FormArray(
              (this.Recipe.steps != null && this.Recipe.steps.length != 0 ? this.Recipe.steps.map(
                m => new FormGroup({
                  title: new FormControl(m.title),
                  description: new FormControl(m.description),
                  //image: new FormControl(m.image),
                })
              ) : [])),
            additionalTips: new FormArray(
              (this.Recipe.additionalTips != null && this.Recipe.additionalTips.length != 0 ? this.Recipe.additionalTips.map(m => new FormControl(m.name)) : [])),
          })
          for(let i = 0; i < this.Recipe.steps.length; i++) {
            if(this.Recipe.steps[i].image != null)
            {
              this.images.push(new FormData())
              this.images[i].append('source', this.BaseUrl+this.Recipe.steps[i].image)
              this.isImageSaved.push(true);
            }
            else{
              this.images.push(new FormData())
              this.isImageSaved.push(false);
            }
          }


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
  get preparationTips() { return this.formGr.get('preparationTips') as FormArray; }
  get steps() { return this.formGr.get('steps') as FormArray; }
  get additionalTips() { return this.formGr.get('additionalTips') as FormArray; }
  images: FormData[] = [];
  isImageSaved: boolean[] = [];

  stepsGr(id: number) { return this.steps.controls[id] }


  changeDifficulty(ev: number){
    this.difficulty.setValue(ev)
  }

  addprep() {
    this.preparationTips.push(new FormControl(''));
  }

  remprep(id: number) {
    this.preparationTips.removeAt(id);
  }

  addaddit() {
    this.additionalTips.push(new FormControl(''));
  }

  remaddit(id: number) {
    this.additionalTips.removeAt(id);
  }

  addinstr() {
    this.steps.push(new FormGroup({
      title: new FormControl(''),
      description: new FormControl('')
    }));
    this.images.push(new FormData())
    this.isImageSaved.push(false);
  }

  reminstr(id: number) {
    this.steps.removeAt(id)
    this.images.splice(id, 1)
    this.isImageSaved.splice(id, 1);
  }

  remPhoto(id: number){
    this.images[id] = new FormData();
    this.isImageSaved[id] = false;
  }

  uploadDocument(id: number, event: any) {
    if (event.target.files && event.target.files[0]) {
      this.images[id].append('file', event.target.files[0])
      let source = URL.createObjectURL(event.target.files[0])
      this.images[id].append('source', source)
      this.isImageSaved[id] = true;
    }
  }

  getEmbedUrl(){
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.Recipe.video)
  }

  Submit(){
    console.log("And another one")
    for (const field in this.formGr.controls) { // 'field' is a string
      console.log(this.formGr.controls[field].value);
    }
    console.log(this.images)
    console.log(this.isImageSaved)
  }
}
