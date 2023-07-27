import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommentItem } from 'src/app/models/recipe.model';
import * as moment from 'moment';
import { RecipeService } from 'src/app/services/recipe.service';
import { GlobalDataService } from 'src/app/services/globalData.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.scss']
})
export class CommentComponent implements OnInit {

  @Input() comment: CommentItem;
  @Input() recipeId: number;
  @Output() imageClicked: EventEmitter<string> = new EventEmitter()
  
  dateCreated: string | null;
  isRepliesLoaded: boolean = false;
  isAnswering: boolean = false;

  BaseUrl: string = "https://localhost:7137/"

  constructor(private datePipe : DatePipe, private recipeService: RecipeService, public global: GlobalDataService) { }

  ngOnInit(){
    moment.locale('uk');
    this.dateCreated = moment(this.comment.dateCreated).fromNow()
  }

  loadReplies(){
    this.recipeService.getReplyComments(this.comment.id).then(
      response => {
        if(response.code == 200){
          this.comment.replies = response.comments;
          this.isRepliesLoaded = true;
        }
      }
    )
  }

  unloadReplies(){
    this.comment.replies = [];
    this.isRepliesLoaded = false;
  }

  AnswerWord(number: number){
    if((number % 10 === 2 || number % 10 === 3 || number % 10 === 4) && (number > 20 || number < 10))
          return `${number} відповіді`
        else if((number % 10 === 1) && (number > 20 || number < 10))
          return `${number} відповідь`
        else
          return `${number} відповідей`
  }

  cancelAnswer(){
    this.isAnswering = false;
  }

  newChildComment(event: FormData){
    let newComment: CommentItem = {
      id: +event.get('id')!.toString(),
      content: event.get('content')!.toString(),
      image: event.get('image') == null ? null : event.get('image')!.toString(),
      userId: this.global.id!,
      userName: this.global.name!,
      userImage: this.global.photo,
      dateCreated: new Date(),
      isAuthor: true,
      countReplies: 0,
      replies: null
    }
    if(this.comment.replies == null){
      this.comment.replies = [];
    }
    this.comment.replies.splice(0, 0, newComment);
    this.isAnswering = false;
  }

  imageClick(){
    console.log(this.BaseUrl+this.comment.image)
    this.imageClicked.emit(this.BaseUrl+this.comment.image)
  }
}

// id : number,
// content : string,
// image : string | null,
// userId : number,
// userName : string,
// dateCreated: Date,
// isAuthor : boolean,
// countRelpies : number,