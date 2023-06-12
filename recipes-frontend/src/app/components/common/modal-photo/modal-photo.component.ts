import { Component, EventEmitter, Input, Output } from '@angular/core';
import { faXmark } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-modal-photo',
  templateUrl: './modal-photo.component.html',
  styleUrls: ['./modal-photo.component.scss']
})
export class ModalPhotoComponent {
  @Input() Image: string
  @Output() closeModal: EventEmitter<void> = new EventEmitter()

  xIcon = faXmark;

  close(){
    this.closeModal.emit();
  }
}
