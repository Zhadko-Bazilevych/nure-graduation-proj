import { Component, OnInit, ViewEncapsulation, ViewChild  } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/recipe.service';
import { IDropdownSettings, MultiSelectComponent } from 'ng-multiselect-dropdown';
import { FilterService } from 'src/app/services/filter.service';
import { IdItem } from 'src/app/models/idItem';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Options } from '@angular-slider/ngx-slider';
import { faArrowDownWideShort, faArrowDownShortWide, faTriangleExclamation, faClock} from '@fortawesome/free-solid-svg-icons';
import { filter, recipe, patternUpdate } from 'src/app/models/filter.model';
import { PatternService } from 'src/app/services/pattern.service';


@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private recipeService: RecipeService, private filterService: FilterService, private patternService: PatternService) { }

  BaseUrl: string = "https://localhost:7137/"

  @ViewChild('dropdown') dropdown: MultiSelectComponent;

  ingredientList: IdItem[] = [];
  selectedIngredientItems: IdItem[] = [];
  multipleDropdownSettings: IDropdownSettings = {};
  dropdownSettings: IDropdownSettings = {};

  sliderDifficultyOptions: Options = {
    floor: 1,
    ceil: 5,
  }
  difficultyMin: number = 1;
  difficultyMax: number = 5;

  foodList: IdItem[] = [];
  selectedFoodItems: IdItem[] = [];
  dishList: IdItem[] = [];
  selectedDishItems: IdItem[] = [];
  menuList: IdItem[] = [];
  selectedMenuItems: IdItem[] = [];
  sortList: IdItem[] = [
    {id: 1, name: "Назва" },
    {id: 2, name: "Складність" },
    {id: 3, name: "Калорійність" },
    {id: 4, name: "Рейтинг" },
    {id: 5, name: "Кількість оцінок" },
    {id: 6, name: "Необхідний час" },
    {id: 7, name: "Нещодавні" },
  ]
  selectedSortItem: IdItem[]= [{ id: 7, name: "Нещодавні"}];

  patternList: IdItem[] = [];
  selectedPatternList: IdItem[] = [];
  patternData: filter[] = [];
  isPatternChosen: boolean = false;
  isPatternCreating: boolean = false;
  isPatternSaved: boolean = false;
  isAuth: boolean = false;


  isDescending: boolean = false;
  descIcon = faArrowDownWideShort;
  ascIcon = faArrowDownShortWide

  forma = new FormGroup({
    name: new FormControl(),
    requiredTimeMin: new FormControl(),
    requiredTimeMax: new FormControl(),
    asIngredientPool: new FormControl(),
    patternName: new FormControl(),
  })
  get name() { return this.forma.get('name') }
  get requiredTimeMin() { return this.forma.get('requiredTimeMin') }
  get requiredTimeMax() { return this.forma.get('requiredTimeMax') }
  get asIngredientPool() { return this.forma.get('asIngredientPool') }
  get patternName() { return this.forma.get('patternName') }

  Recipes: recipe[];

  difficultyIcon = faTriangleExclamation;
  clockIcon = faClock;

  ngOnInit() {
    this.onSearchChange("");
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

    this.multipleDropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      allowSearchFilter: true,
      enableCheckAll: false,
      searchPlaceholderText: "Пошук...",
      allowRemoteDataSearch: true,
      limitSelection: 10,
      maxHeight: 999,
    };

    this.dropdownSettings = {
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

    this.forma.controls.asIngredientPool.setValue('false');

    this.patternService.getPatternList().toPromise().then(
      response => {
        if(response.code == 200)
        {
          this.patternList = response.items
          const createNew: IdItem = { id: -1, name: "<Новий шаблон>"}
          this.patternList.push(createNew)
          this.patternData = response.patterns
          this.isAuth = true;
        }
      }
    );

    this.Filtered()
  }

  searchTxt:string = '';
  timeout: ReturnType<typeof setTimeout> | undefined | null = null;

  onFilterChange(item: any) {
    clearTimeout(this.timeout!);
    this.timeout = setTimeout(() => {
      this.onSearchChange(item);
    }, 1000);
  }

  onSearchChange(filter: string) {
    this.filterService.getIngredients(filter).toPromise().then(
      response => {
        if (response.code == 200) {
          this.ingredientList = response.ingredients
      }
      }
    )
  }

  async Filtered() {
    if (this.forma.valid) {
      const filterRequest = {
        name: this.name?.value,
        requiredTimeMin: this.requiredTimeMin?.value,
        requiredTimeMax: this.requiredTimeMax?.value,
        asIngredientPool: this.asIngredientPool?.value == "true" ? true : false,
        sortType: this.selectedSortItem[0]?.name,
        difficultyMin: this.difficultyMin,
        difficultyMax: this.difficultyMax,
        dishTypeId: this.selectedDishItems.map(m => m.id),
        foodTypeId: this.selectedFoodItems.map(m => m.id),
        menuTypeId: this.selectedMenuItems.map(m => m.id),
        ingredientId: this.selectedIngredientItems.map(m => m.id),
        isDescending: this.isDescending
      }
      this.filterService.filter(filterRequest).toPromise().then(
        response => { 
          if(response.code == 200)
          this.Recipes = response.recipes 
         }
      )
      
    }
  }

  recipe(id: number){
    this.router.navigate([`/recipe`, id]);
  }

  patternSelect(ev: any){
    if(ev.id == -1){
      this.isPatternCreating = true;
      this.isPatternChosen = false;
      this.selectedPatternList = [];
    }
    else{
        this.isPatternChosen = true;

        const chosenFilter: filter = this.patternData[this.patternList.findIndex(x=>x.id == ev.id)];
        
        this.name!.setValue(chosenFilter.name);
        this.requiredTimeMin!.setValue(chosenFilter.requiredTimeMin);
        this.requiredTimeMax!.setValue(chosenFilter.requiredTimeMax);
        this.asIngredientPool!.setValue(String(chosenFilter.asIngredientPool));
        this.selectedIngredientItems = chosenFilter.ingredientId??[];
        this.difficultyMin = chosenFilter.difficultyMin;
        this.difficultyMax = chosenFilter.difficultyMax;
        this.selectedFoodItems = chosenFilter.foodTypeId??[];
        this.selectedDishItems = chosenFilter.dishTypeId??[];
        this.selectedMenuItems = chosenFilter.menuTypeId??[];
        this.isDescending = chosenFilter.isDescending;
        this.selectedSortItem = [this.sortList.find(x=>x.name==chosenFilter.sortType??"Нещодавні")!]  
    }

  }

  patternDeselect(ev: any){
    this.selectedPatternList = [this.patternList.find(x => x.id == ev.id)!]
  }

  patternDelete(){
    let id = this.selectedPatternList[0].id;
    this.patternService.deletePattern(id).toPromise().then(
      response => { 
        if(response.code == 200)
        {
          let index = this.patternList.findIndex(x => x.id == id);

          this.patternList = this.patternList.slice(0, index).concat(this.patternList.slice(index+1,this.patternList.length+1));
          this.patternData = this.patternData.slice(0, index).concat(this.patternData.slice(index+1,this.patternData.length+1));
          
          this.isPatternChosen = false;
          this.selectedPatternList = [];
        }
       }
    )
  }

  patternUpdate(){
    let selectedId = (this.isPatternChosen ? this.selectedPatternList[0]!.id : null)
    let selectedName = (this.isPatternChosen ? this.selectedPatternList[0]!.name : this.patternName?.value)

    let pattern: patternUpdate = {
      id: selectedId,
      patternName: selectedName,
      name: this.name?.value,
      requiredTimeMin: this.requiredTimeMin?.value,
      requiredTimeMax: this.requiredTimeMax?.value,
      asIngredientPool: this.asIngredientPool?.value == "true" ? true : false,
      sortType: this.selectedSortItem[0]?.name,
      difficultyMin: this.difficultyMin,
      difficultyMax: this.difficultyMax,
      dishTypeId: this.selectedDishItems.map(m => m.id),
      foodTypeId: this.selectedFoodItems.map(m => m.id),
      menuTypeId: this.selectedMenuItems.map(m => m.id),
      ingredientId: this.selectedIngredientItems.map(m => m.id),
      isDescending: this.isDescending
    }
    console.log(selectedName);
    if(this.isPatternChosen || selectedName != null)
    {
      this.patternService.updatePattern(pattern).toPromise().then(
        response => { 
          if(response.code == 200) 
          {
            this.isPatternSaved = true;
            setTimeout(()=>{this.isPatternSaved = false;}, 2000)
            let updated: filter = {
              name: this.name?.value,
              requiredTimeMin: this.requiredTimeMin?.value,
              requiredTimeMax: this.requiredTimeMax?.value,
              asIngredientPool: this.asIngredientPool?.value == "true" ? true : false,
              sortType: this.selectedSortItem[0]?.name,
              difficultyMin: this.difficultyMin,
              difficultyMax: this.difficultyMax,
              dishTypeId: this.selectedDishItems,
              foodTypeId: this.selectedFoodItems,
              menuTypeId: this.selectedMenuItems,
              ingredientId: this.selectedIngredientItems,
              isDescending: this.isDescending
            }
            if(selectedId != null)
            {
              let selectedListIndex = this.selectedPatternList.findIndex(x=>x.id == selectedId)
              this.patternData[selectedListIndex] = updated
              this.patternList[selectedListIndex].name = selectedName
            }
            else
            {
              this.patternData.push(updated)
              this.patternList[this.patternList.length-1] = {id: response.id, name: selectedName!};
              this.patternList.push({ id: -1, name: "<Новий шаблон>"})
              this.isPatternCreating = false;
              this.isPatternChosen = true;
              this.patternName?.setValue(null);
              this.selectedPatternList = [{id: response.id, name: selectedName!}];
            }
          }
        }
      )
    }
  }

  patternCancel(){
    this.patternName!.setValue(null);
    this.isPatternCreating = false;
    this.isPatternChosen = false;
    this.selectedPatternList = [];
  }
}