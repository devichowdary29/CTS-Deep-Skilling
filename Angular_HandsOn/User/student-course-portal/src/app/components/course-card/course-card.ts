import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Highlight } from '../../directives/highlight';
import { CreditLabelPipe } from '../../pipes/credit-label-pipe';

export interface Course {
  id: number;
  name: string;
  code: string;
  credits: number;
  gradeStatus?: string;
  isEnrolled?: boolean;
}

@Component({
  selector: 'app-course-card',
  imports: [CommonModule, Highlight, CreditLabelPipe],
  templateUrl: './course-card.html',
  styleUrl: './course-card.css',
})
export class CourseCard implements OnChanges {
  @Input() course!: Course;
  @Output() enrollRequested = new EventEmitter<number>();
  
  isExpanded = false;

  get cardClasses() {
    return {
      'card--enrolled': this.course?.isEnrolled,
      'card--full': this.course?.credits >= 4,
      'expanded': this.isExpanded
    };
  }

  getBorderColor() {
    switch(this.course?.gradeStatus) {
      case 'passed': return 'green';
      case 'failed': return 'red';
      case 'pending': return 'grey';
      default: return 'transparent';
    }
  }

  toggleExpanded() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['course']) {
      console.log('Course changed from', changes['course'].previousValue, 'to', changes['course'].currentValue);
    }
  }
}
