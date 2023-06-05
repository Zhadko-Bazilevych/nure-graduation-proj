import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faBook, faStar } from '@fortawesome/free-solid-svg-icons';
import { recipe } from 'src/app/models/filter.model';
import { Author } from 'src/app/models/user.model';

@Component({
  selector: 'app-author-row',
  templateUrl: './author-row.component.html',
  styleUrls: ['./author-row.component.scss']
})
export class AuthorRowComponent {

  @Input() item: Author;
  @Output() onClick: EventEmitter<any> = new EventEmitter();
  @Output() onEditClick: EventEmitter<any> = new EventEmitter();
  @Output() onDeleteClick: EventEmitter<any> = new EventEmitter();
  @Input() editable: boolean = false;
  
  recipesIcon = faBook;
  subsIcon = faStar;
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
