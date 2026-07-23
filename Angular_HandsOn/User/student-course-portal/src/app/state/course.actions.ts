import { createAction, props } from '@ngrx/store';
import { Course } from '../models/course.model';

export const loadCourses = createAction('[Course] Load Courses');
export const loadCoursesSuccess = createAction('[Course] Load Courses Success', props<{ courses: Course[] }>());
export const loadCoursesFailure = createAction('[Course] Load Courses Failure', props<{ error: any }>());

export const enrollCourse = createAction('[Course] Enroll Course', props<{ courseId: number }>());
export const unenrollCourse = createAction('[Course] Unenroll Course', props<{ courseId: number }>());
