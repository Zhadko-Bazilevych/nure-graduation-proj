import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { CommentItem } from 'src/app/models/recipe.model';
import { GlobalDataService } from 'src/app/services/globalData.service';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-comment-edit',
  templateUrl: './comment-edit.component.html',
  styleUrls: ['./comment-edit.component.scss']
})
export class CommentEditComponent implements OnInit {
  
  @Input() recipeId: number
  @Input() parentId: number;
  @Input() canCancel: boolean = false
  @Output() commentCreated: EventEmitter<FormData> = new EventEmitter()
  @Output() canceled: EventEmitter<any> = new EventEmitter()

  BaseUrl: string = "https://localhost:7137/"

  formGr: FormGroup = new FormGroup({
    content: new FormControl('', [Validators.required])
  })

  get content() { return this.formGr.get('content')! }

  newComment: FormData;
  tempPhotoUrl: string | null;

  xIcon = faXmark;

  constructor(private recipeService: RecipeService, public global: GlobalDataService) { }

  ngOnInit(){
  }

  insertedPhoto(event: any){
    console.log(event)
    if (event.target.files && event.target.files[0]) {
      this.newComment = new FormData()
      this.newComment.append('image', event.target.files[0]);
      this.tempPhotoUrl = URL.createObjectURL(event.target.files[0]);
    }
  }

  remPhoto(){
    this.tempPhotoUrl = null;
    this.newComment.delete('image');
  }

  Cancel(){
    this.canceled.emit();
  }

  Submit(){
    if(this.parentId != null) {
      this.newComment.append('parentCommentId', this.parentId.toString())
    }
    this.newComment.append('recipeId', this.recipeId.toString())
    this.newComment.append('userId', this.global.id!.toString())
    this.newComment.append('content', this.content.value.toString())
    this.recipeService.createComment(this.newComment).then(
      response => {
        if(response.code == 200){
          this.newComment.append('id', response.id.toString());
          this.newComment.delete('image');
          if(response.image != null){
            this.newComment.append('image', response.image)
          }
          this.commentCreated.emit(this.newComment);
        }
      }
    )
  }
}