import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Course } from '../../models/course.model';
import { selectEnrolledCourses } from '../../state/course.selectors';
import * as CourseActions from '../../state/course.actions';

@Component({
  selector: 'app-student-profile',
  imports: [CommonModule, RouterModule],
  templateUrl: './student-profile.html',
  styleUrl: './student-profile.css',
})
export class StudentProfile implements OnInit {
  enrolledCourses$!: Observable<Course[]>;

  constructor(private store: Store) {}

  ngOnInit() {
    // Dispatching loadCourses just in case this page is loaded first directly
    this.store.dispatch(CourseActions.loadCourses());
    this.enrolledCourses$ = this.store.select(selectEnrolledCourses);
  }
}
