import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faClock, faTriangleExclamation } from '@fortawesome/free-solid-svg-icons';
import { recipe } from 'src/app/models/filter.model';

@Component({
  selector: 'app-recipe-row',
  templateUrl: './recipe-row.component.html',
  styleUrls: ['./recipe-row.component.scss']
})
export class RecipeRowComponent {
  @Input() item: recipe;
  @Output() onClick: EventEmitter<any> = new EventEmitter();
  @Output() onEditClick: EventEmitter<any> = new EventEmitter();
  @Output() onDeleteClick: EventEmitter<any> = new EventEmitter();
  @Input() editable: boolean = false;
  
  difficultyIcon = faTriangleExclamation;
  clockIcon = faClock;
  BaseUrl: string = "https://localhost:7137/"

  itemClicked(){
    this.onClick.emit();
  }

  itemEditClicked(ev: Event){
    ev.stopPropagation();
    this.onEditClick.emit();
  }

  itemDeleteClicked(ev: Event){
    ev.stopPropagation();
    this.onDeleteClick.emit();
  }
}
