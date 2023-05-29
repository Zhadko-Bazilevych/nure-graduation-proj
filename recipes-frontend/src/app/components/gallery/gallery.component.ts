import { Component, Input, OnInit } from '@angular/core';
import { faCircleChevronLeft, faCircleChevronRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})

export class GalleryComponent implements OnInit {
  @Input() Images: string[];
  index: number = 0;
  BaseUrl: string = "https://localhost:7137/"

  leftArrow = faCircleChevronLeft;
  rightArrow = faCircleChevronRight;
  
  constructor() { }
  
  ngOnInit(): void {

  }

  clickHandler(i: number) {
    this.index = i
  }
}
