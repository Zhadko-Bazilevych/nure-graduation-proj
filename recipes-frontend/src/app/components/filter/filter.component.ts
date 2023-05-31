import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from 'src/app/services/recipe.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FilterService } from 'src/app/services/filter.service';
import { IdItem } from 'src/app/models/idItem';
import { FormControl, FormGroup } from '@angular/forms';
import { Options } from '@angular-slider/ngx-slider';
import { faArrowUpShortWide, faArrowDownShortWide} from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss'],
})
export class FilterComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private recipeService: RecipeService, private filterService: FilterService) { }

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

  isDescending: boolean = false;
  descIcon = faArrowUpShortWide;
  ascIcon = faArrowDownShortWide

  forma = new FormGroup({
    name: new FormControl(),
    requiredTimeMin: new FormControl(),
    requiredTimeMax: new FormControl(),
    asIngredientPool: new FormControl(),
    sortType: new FormControl(),
  })
  get name() { return this.forma.get('name') }
  get requiredTimeMin() { return this.forma.get('requiredTimeMin') }
  get requiredTimeMax() { return this.forma.get('requiredTimeMax') }
  get asIngredientPool() { return this.forma.get('asIngredientPool') }
  get sortType() { return this.forma.get('sortType') }

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

    this.forma.controls.asIngredientPool.setValue(false);

    // this.Filtered()
  }

  searchTxt:string = '';
  timeout: ReturnType<typeof setTimeout> | undefined | null = null;

  onFilterChange(item: any) {
    // console.log("Start")
    // console.log(this.difficultyMax)
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
        asIngredientPool: this.asIngredientPool?.value,
        sortType: this.sortType?.value,
        difficultyMin: this.difficultyMin,
        difficultyMax: this.difficultyMax,
        dishTypeId: this.selectedDishItems.map(m => m.id),
        foodTypeId: this.selectedFoodItems.map(m => m.id),
        menuTypeId: this.selectedMenuItems.map(m => m.id),
        ingredientId: this.selectedIngredientItems.map(m => m.id),
        isDescending: this.isDescending
      }
      this.filterService.filter(filterRequest).toPromise().then(
        response => { console.log(response) }
      )
      
    }
  }
}