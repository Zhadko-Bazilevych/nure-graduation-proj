import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { faBacon, faBowlFood, faClock, faDroplet, faStar, faTriangleExclamation, faUtensils, faWheatAwn, faXmark } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkOut } from '@fortawesome/free-solid-svg-icons';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { IdItem } from 'src/app/models/idItem';
import { RecipeInfo, RecipeUpdateData, RecipeUpdateInfo } from 'src/app/models/recipe.model';
import { FilterService } from 'src/app/services/filter.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { UserRecipeService } from 'src/app/services/userRecipe.service';

@Component({
  selector: 'app-recipe-update',
  templateUrl: './recipe-update.component.html',
  styleUrls: ['./recipe-update.component.scss']
})
export class RecipeUpdateComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private recipeService: RecipeService, private filterService: FilterService, private userRecipeService: UserRecipeService, private sanitizer: DomSanitizer) { }

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
  isUrlValidating: boolean = true;
  isUrlValid: boolean = true;
  
  Recipe: RecipeUpdateInfo;
  RecipeImages: string[] | null

  formGr: FormGroup;
  isLoadingForm: boolean = true;
  isSubmit: boolean = false;

  dropdownSettings: IDropdownSettings = {};
  dropdownSettingsRemote: IDropdownSettings = {};
  dropdownSettingsWithoutSearch: IDropdownSettings = {};
  multipleDropdownSettings: IDropdownSettings = {};

  foodList: IdItem[] = [];
  dishList: IdItem[] = [];
  menuList: IdItem[] = [];
  measurementList: IdItem[] = [];
  ingredientList: IdItem[] = [];


  ngOnInit(): void {
    let param = this.route.snapshot.paramMap.get('id')
    let id = param == null ? -1 : +param
    this.recipeService.updateRecipeInfo(id).then(
      response => {
        if(response.code == 200){
          this.Recipe = response.recipe;
          this.RecipeImages = this.Recipe?.images.map(s => this.BaseUrl + s.name);
          this.sanitizedVideo = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.youtube.com/embed/" + this.Recipe.video)
          this.formGr = new FormGroup({
            name: new FormControl(this.Recipe.name, { validators: [Validators.required]}),
            requiredTime: new FormControl(this.Recipe.requiredTime, { validators: [Validators.required]}),
            servings: new FormControl(this.Recipe.servings, { validators: [Validators.required]}),
            difficulty: new FormControl(this.Recipe.difficulty, { validators: [Validators.required]}),
            caloricValue: new FormControl(this.Recipe.caloricValue),
            proteins: new FormControl(this.Recipe.proteins),
            fats: new FormControl(this.Recipe.fats),
            carbohydrates: new FormControl(this.Recipe.carbohydrates),
            foodType: new FormControl([this.Recipe.foodType], { validators: [requiredList]}),
            dishType: new FormControl([this.Recipe.dishType], { validators: [requiredList]}),
            menuType: new FormControl(this.Recipe.menuTypes),
            description: new FormControl(this.Recipe.description),
            preparationTips: new FormArray(
              (this.Recipe.preparationTips != null && this.Recipe.preparationTips.length != 0 ? this.Recipe.preparationTips.map(m => new FormControl(m.name)) : [])),
            steps: new FormArray(
              (this.Recipe.steps != null && this.Recipe.steps.length != 0 ? this.Recipe.steps.map(
                m => new FormGroup({
                  id: new FormControl(m.id),
                  title: new FormControl(m.title, [Validators.required]),
                  description: new FormControl(m.description, [Validators.required]),
                })
              ) : [])),
            additionalTips: new FormArray(
              (this.Recipe.additionalTips != null && this.Recipe.additionalTips.length != 0 ? this.Recipe.additionalTips.map(m => new FormControl(m.name)) : [])),
            videoUrl: new FormControl(this.Recipe.video == null ? '' : "https://www.youtube.com/watch?v="+this.Recipe.video),
            ingredients: new FormArray(
              (this.Recipe.ingredients != null && this.Recipe.ingredients.length != 0 ? this.Recipe.ingredients.map(
                m => new FormGroup({
                  ingredient: new FormControl({id: m.ingredientId, name: m.name}, { validators: [Validators.required] }),
                  measurement: new FormControl([{id: m.measurementId, name: m.measurement}], { validators: [requiredList] }),
                  amount: new FormControl(m.amount),
                })
              ) : [])),
            newIngredient: new FormControl([] as IdItem[]),
          });

          for(let i = 0; i < this.Recipe.steps.length; i++) {
            if(this.Recipe.steps[i].image != null)
            {
              this.stepImages.push(new FormData())
              this.stepImages[i].append('source', this.BaseUrl+this.Recipe.steps[i].image)
              this.isStepImageSaved.push(true);
            }
            else{
              this.stepImages.push(new FormData())
              this.isStepImageSaved.push(false);
            }
          }

          for(let i = 0; i < this.Recipe.images.length; i++) {
            this.galleryImages.push(new FormData())
            this.galleryImages[i].append('index', this.Recipe.images[i].id.toString())
          }
          this.videoUrlChange()


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

    this.userRecipeService.getMeasurementsData().toPromise().then(
      response => {
        if(response.code == 200)
        {
          this.measurementList = response.measurements;
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

    this.dropdownSettingsRemote = {
      singleSelection: true,
      closeDropDownOnSelection: true,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: "Пошук...",
      allowRemoteDataSearch: true,
      limitSelection: 10,
      maxHeight: 999,
    }
    
    this.dropdownSettingsWithoutSearch = {
      singleSelection: true,
      closeDropDownOnSelection: true,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: false,
      enableCheckAll: false,
      searchPlaceholderText: "Пошук...",
      allowRemoteDataSearch: false,
      limitSelection: 10,
      maxHeight: 999,
    };

    this.onFilterChange('');
  }

  get name() { return this.formGr.get('name')! }
  get difficulty() { return this.formGr.get('difficulty')! }
  get requiredTime() { return this.formGr.get('requiredTime')! }
  get servings() { return this.formGr.get('servings')! }
  get caloricValue() { return this.formGr.get('caloricValue')! }
  get proteins() { return this.formGr.get('proteins')! }
  get fats() { return this.formGr.get('fats')! }
  get carbohydrates() { return this.formGr.get('carbohydrates')! }
  get foodType() { return this.formGr.get('foodType')! }
  get dishType() { return this.formGr.get('dishType')! }
  get menuType() { return this.formGr.get('menuType')!}
  get preparationTips() { return this.formGr.get('preparationTips') as FormArray; }
  get steps() { return this.formGr.get('steps') as FormArray; }
  get additionalTips() { return this.formGr.get('additionalTips') as FormArray; }
  get videoUrl() { return this.formGr.get('videoUrl')! }
  stepImages: FormData[] = [];
  isStepImageSaved: boolean[] = [];
  backendStepImageToDelete: number[] = [];

  galleryImages: FormData[] = [];
  get ingredients() { return this.formGr.get('ingredients') as FormArray; }
  get newIngredient() { return this.formGr.get('newIngredient')! }
  get description() { return this.formGr.get('description')!}

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
      id: new FormControl(0),
      title: new FormControl(''),
      description: new FormControl('')
    }));
    this.stepImages.push(new FormData())
    this.isStepImageSaved.push(false);
  }

  reminstr(id: number) {
    if(this.steps.controls[id].get('id')!.value != 0 ) {
      this.backendStepImageToDelete.push(this.steps.controls[id].get('id')!.value);
    }

    this.steps.removeAt(id)
    this.stepImages.splice(id, 1)
    this.isStepImageSaved.splice(id, 1);
  }

  remPhoto(id: number){
    this.stepImages[id] = new FormData();
    this.isStepImageSaved[id] = false;

    if(this.steps.controls[id].get('id')!.value != 0 ) {
      this.backendStepImageToDelete.push(this.steps.controls[id].get('id')!.value);
    }
  }

  uploadDocument(id: number, event: any) {
    if (event.target.files && event.target.files[0]) {
      this.stepImages[id].append('file', event.target.files[0])
      let source = URL.createObjectURL(event.target.files[0])
      this.stepImages[id].append('source', source)
      this.isStepImageSaved[id] = true;
    }
  }

  insgal(ev: FormData) {
    this.galleryImages.push(ev);
  }

  delgal(ev: number) {
    this.galleryImages.splice(ev, 1);
  }

  remingr(id: number){
    this.ingredients.removeAt(id);
    this.onSearchChange('');
  }

  addingr(ev: any){
    if(ev.id != -1) {
      this.ingredients.push(new FormGroup({
        ingredient: new FormControl(ev),
        measurement: new FormControl(''),
        amount: new FormControl(0),
      }));
    }
    else {
      let newName = ev.name.split("'")[1];
      this.recipeService.addIngredient(newName).then(
        response => {
          if(response.code == 200) {
            this.ingredients.push(new FormGroup({
              ingredient: new FormControl({id:response.id, name:newName}),
              measurement: new FormControl(''),
              amount: new FormControl(0),
            }));
          }
        }
      )
    }
    this.newIngredient.reset();
    this.onSearchChange('');
  }

  timeout: ReturnType<typeof setTimeout> | undefined | null = null;

  onFilterChange(item: any) {
    clearTimeout(this.timeout!);
    this.timeout = setTimeout(() => {
      this.onSearchChange(item);
    }, 1000);
  }

  createActive: boolean = false;
  onSearchChange(filter: string) {
    let selectedIngredientIdList = this.ingredients.controls.map( m => {
      return m.get('ingredient')!.value.id as number
    });
    this.filterService.getIngredients(filter, selectedIngredientIdList).toPromise().then(
      response => {
        if (response.code == 200) {
          this.ingredientList = response.ingredients
          if(this.ingredientList.length < 10 && this.ingredientList.filter(f=>f.name == filter).length == 0 && filter != ''){
            let cut = filter.slice(0,17)
            this.ingredientList.push({id: -1, name:`Створити новий ігредієнт '${cut}' `})
            this.createActive = true;
          }
          else{
            this.createActive = false;
          }
      }
      }
    )
  }

  async Submit(isPublic: boolean){
      let data: FormData = new FormData()
      data.append('id', this.Recipe.id.toString());
      if(this.name.valid){ data.append('name', this.name!.value); }
      if(this.description.value != null){ data.append('description', this.description!.value); }
      if(this.difficulty.value != null){ data.append('difficulty', this.difficulty!.value); }
      if(this.requiredTime.value != null){ data.append('requiredTime', this.requiredTime!.value); }
      if(this.servings.value != null){ data.append('servings', this.servings!.value); }
      if(this.caloricValue.value != null){ data.append('caloricValue', this.caloricValue!.value); }
      if(this.proteins.value != null){ data.append('proteins', this.proteins!.value); }
      if(this.fats.value != null){ data.append('fats', this.fats!.value); }
      if(this.carbohydrates.value != null){ data.append('carbohydrates', this.carbohydrates!.value); }
      if(await ValidateUrl(this.videoUrl) == null){ data.append('video', this.videoUrl!.value.split('=')[1].toString()); }
      if(this.foodType.valid){ data.append('foodType', this.foodType!.value[0].id); }
      if(this.dishType.valid){ data.append('dishType', this.dishType!.value[0].id); }
      data.append('isPublished', isPublic.toString());
      // data.append('preparationTips', JSON.stringify(this.preparationTips!.value));
      // data.append('additionalTips', JSON.stringify(this.additionalTips!.value));
      // data.append('menuTypes', JSON.stringify(this.menuType!.value.map( (m: IdItem) => m.id )));

      // data.append('ingredientsId', JSON.stringify(this.ingredients!.value.map( (m: {ingredient: {id: number, name: string}, measurement: {id: number, name: string}[], amount: number}) => m.ingredient.id.toString())));
      // data.append('ingredientsMeasurementId', JSON.stringify(this.ingredients!.value.map( (m: {ingredient: {id: number, name: string}, measurement: {id: number, name: string}[], amount: number}) => m.measurement[0].id.toString())));
      // data.append('ingredientsAmount', JSON.stringify(this.ingredients!.value.map( (m: {ingredient: {id: number, name: string}, measurement: {id: number, name: string}[], amount: number}) => m.amount.toString())));
      // data.append('stepsIds', JSON.stringify(this.steps!.value.map( (m: {id: number | null, title: string, description: string}) => (m.id ?? 0).toString())));
      // data.append('stepsTitles', JSON.stringify(this.steps!.value.map( (m: {id: number,title: string, description: string}) => m.title)));
      // data.append('stepsDescriptions', JSON.stringify(this.steps!.value.map( (m: {id: number, title: string, description: string}) => m.description)));
      for (let i = 0; i < this.stepImages.length; i++) {
        data.append('stepImagesData', this.stepImages[i].get('file') as Blob | null ?? new Blob(), `file${i + 1}.png`);
      }
      for (let i = 0; i < this.galleryImages.length; i++) {
        data.append('imagesIndexes', this.galleryImages[i].get('index') as string);
      }
      
      for (let i = 0; i < this.galleryImages.length; i++) {
        data.append('imagesData', this.galleryImages[i].get('file') as Blob | null ?? new Blob(), `file${i + 1}.png`);
      }
      //ANOTHER WAY
      for (let i = 0; i < this.preparationTips!.value.length; i++) {
        data.append('preparationTips', this.preparationTips!.value[i]);
      }
      for (let i = 0; i < this.additionalTips!.value.length; i++) {
        data.append('additionalTips', this.additionalTips!.value[i]);
      }


      for (let i = 0; i < this.menuType!.value.length; i++) {
        data.append('menuTypes', this.menuType!.value[i].id.toString());
      }
      for (let i = 0; i < this.ingredients!.value.length; i++) {
        data.append('ingredientsMeasurementId', this.ingredients!.value[i].measurement[0].id.toString());
      }
      for (let i = 0; i < this.ingredients!.value.length; i++) {
        data.append('ingredientsId', this.ingredients!.value[i].ingredient.id.toString());
      }
      for (let i = 0; i < this.ingredients!.value.length; i++) {
        data.append('ingredientsAmount', this.ingredients!.value[i].amount.toString());
      }
      for (let i = 0; i < this.steps!.value.length; i++) {
        data.append('stepsIds', (this.steps!.value[i].id??0).toString());
      }

      for (let i = 0; i < this.steps!.value.length; i++) {
        data.append('stepsTitles', (this.steps!.value[i].title).toString());
      }
      for (let i = 0; i < this.steps!.value.length; i++) {
        data.append('stepsDescriptions', (this.steps!.value[i].description).toString());
      }
      for (let i = 0; i < this.backendStepImageToDelete.length; i++) {
        data.append('backendStepImageToDelete', (this.backendStepImageToDelete[i]).toString());
      }

      if(!isPublic){
        this.recipeService.sendSomewhere(data).then(
          r => { this.router.navigate(['useractions/3']) }
        );
      }
      else{
        this.isSubmit = true;
        if (this.formGr.valid){
          this.recipeService.sendSomewhere(data).then(
            r => { this.router.navigate([`recipe/${this.Recipe.id}`]) }
          );
        }
      }


    // console.log("And another one")
    // for (const field in this.formGr.controls) { // 'field' is a string
    //   console.log(this.formGr.controls[field].value);
    // }
    // console.log(this.stepImages)
    // console.log(this.isStepImageSaved)
    // console.log(this.galleryImages[0].get('index'))
    // console.log(this.galleryImages.map(i => i.get('index')))
    // console.log(this.galleryImages.map(i => i.get('file')))
    // console.log(this.isUrlValid, 'url')
  }


  videoUrlChange(){
    this.isUrlValid = true;
    this.isUrlValidating = true;
    let url = this.videoUrl.value as string;
    var img = new Image();
		img.src = "http://img.youtube.com/vi/" + url.split('=')[1] + "/mqdefault.jpg";
		img.onload = () => {
			if(img.width === 120) {
        this.isUrlValid = false;
      }
      this.isUrlValidating = false;
		}
    this.sanitizedVideo = this.sanitizer.bypassSecurityTrustResourceUrl(url.replace('watch?v=', 'embed/'))
  }

}

export async function ValidateUrl(control: AbstractControl): Promise<ValidationErrors | null> {
  const url = control.value as string;
  const videoId = url.split('=')[1]
  const imageUrl = `http://img.youtube.com/vi/${videoId}/mqdefault.jpg`;

  return new Promise<ValidationErrors | null>((resolve) => {
    const img = new Image();

    img.onload = () => {
      if (img.width === 120) {
        resolve({ invalidUrl: true });
      } else {
        resolve(null);
      }
    };

    img.onerror = () => {
      resolve({ invalidUrl: true });
    };

    img.src = imageUrl;
  });
}

function requiredList(fromControl: AbstractControl) {
  return fromControl.value && fromControl.value.length && fromControl.value[0] != null ? null : {
    requiredList: {
      valid: false
    }
  };
} 