import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseCard } from '../../components/course-card/course-card';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Course } from '../../models/course.model';
import * as CourseActions from '../../state/course.actions';
import { selectCoursesWithEnrollment, selectCoursesLoading, selectCoursesError } from '../../state/course.selectors';

@Component({
  selector: 'app-course-list',
  imports: [CommonModule, CourseCard, RouterModule, FormsModule],
  templateUrl: './course-list.html',
  styleUrl: './course-list.css',
})
export class CourseList implements OnInit {
  searchTerm = '';
  selectedCourseId: number | null = null;
  
  courses$!: Observable<Course[]>;
  isLoading$!: Observable<boolean>;
  error$!: Observable<any>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private store: Store
  ) {}

  ngOnInit() {
    this.searchTerm = this.route.snapshot.queryParamMap.get('search') || '';
    
    this.store.dispatch(CourseActions.loadCourses());
    
    this.courses$ = this.store.select(selectCoursesWithEnrollment);
    this.isLoading$ = this.store.select(selectCoursesLoading);
    this.error$ = this.store.select(selectCoursesError);
  }

  onSearchChange() {
    this.router.navigate(['courses'], { queryParams: { search: this.searchTerm } });
  }

  trackByCourseId(index: number, course: any): number {
    return course.id;
  }

  onEnroll(courseId: number) {
    console.log('Enrolling in course: ' + courseId);
    this.selectedCourseId = courseId;
    this.store.dispatch(CourseActions.enrollCourse({ courseId }));
  }
}
