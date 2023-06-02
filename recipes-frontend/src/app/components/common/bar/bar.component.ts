import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-bar',
  templateUrl: './bar.component.html',
  styleUrls: ['./bar.component.scss']
})
export class DifficultyComponent {
  @Input() value: number;
  
  numSequence(n: number): Array<number> {
    return Array(n);
  }
}
