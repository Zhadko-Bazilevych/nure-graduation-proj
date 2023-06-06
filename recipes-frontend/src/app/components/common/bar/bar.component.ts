import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-bar',
  templateUrl: './bar.component.html',
  styleUrls: ['./bar.component.scss']
})
export class DifficultyComponent implements OnInit {
  ngOnInit(): void {
    this.currentValue = this.value ?? 0;
  }

  @Input() value: number | null;
  currentValue: number;
  @Input() editable: boolean = false;
  @Output() changed: EventEmitter<any> = new EventEmitter();

  changedValue(val: number){
    this.changed.emit(val);
  }

  hoverBar(n: number) {
    this.currentValue = n;
  }

  unhoverBar() {
    this.currentValue = this.value ?? 0;
  }
}
