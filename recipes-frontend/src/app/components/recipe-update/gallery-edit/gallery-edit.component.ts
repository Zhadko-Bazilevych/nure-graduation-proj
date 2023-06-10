import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Form } from '@angular/forms';
import { faCircleChevronLeft, faCircleChevronRight, faXmark } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-gallery-edit',
  templateUrl: './gallery-edit.component.html',
  styleUrls: ['./gallery-edit.component.scss']
})

export class GalleryEditComponent implements OnInit {
  @Input() Images: string[];
  index: number = 0;
  BaseUrl: string = "https://localhost:7137/"

  @Output() deleted: EventEmitter<number> = new EventEmitter();
  @Output() inserted: EventEmitter<FormData> = new EventEmitter();

  leftArrow = faCircleChevronLeft;
  rightArrow = faCircleChevronRight;
  xIcon = faXmark;

  lastInserted: FormData;

  constructor() { }
  
  ngOnInit(): void {
  }

  clickHandler(i: number) {
    this.index = i
  }

  insertedPhoto(event: any){
    console.log(event)
    if (event.target.files && event.target.files[0]) {
      this.lastInserted = new FormData()
      this.lastInserted.append('file', event.target.files[0]);
      this.lastInserted.append('index', '0');
      this.Images.push(URL.createObjectURL(event.target.files[0]));
      this.inserted.emit(this.lastInserted);
    }
  }

  deletedPhoto(id: number){
    this.Images.splice(id, 1);
    this.deleted.emit(id);
    if(this.index >= this.Images.length) {
      this.index--;
      if(this.index < 0)
      this.index = 0;
    }
  }
}
