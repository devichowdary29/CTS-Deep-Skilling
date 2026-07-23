import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CourseState } from './course.reducer';

export const selectCourseState = createFeatureSelector<CourseState>('course');

export const selectAllCourses = createSelector(
  selectCourseState,
  (state: CourseState) => state.courses
);

export const selectEnrolledIds = createSelector(
  selectCourseState,
  (state: CourseState) => state.enrolledIds
);

export const selectCoursesWithEnrollment = createSelector(
  selectAllCourses,
  selectEnrolledIds,
  (courses, enrolledIds) => courses.map(c => ({
    ...c,
    isEnrolled: enrolledIds.includes(c.id)
  }))
);

export const selectEnrolledCourses = createSelector(
  selectCoursesWithEnrollment,
  (courses) => courses.filter(c => c.isEnrolled)
);

export const selectCoursesLoading = createSelector(
  selectCourseState,
  (state: CourseState) => state.loading
);

export const selectCoursesError = createSelector(
  selectCourseState,
  (state: CourseState) => state.error
);
