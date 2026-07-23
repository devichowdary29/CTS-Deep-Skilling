import { createReducer, on } from '@ngrx/store';
import { Course } from '../models/course.model';
import * as CourseActions from './course.actions';

export interface CourseState {
  courses: Course[];
  enrolledIds: number[];
  loading: boolean;
  error: any;
}

export const initialState: CourseState = {
  courses: [],
  enrolledIds: [],
  loading: false,
  error: null
};

export const courseReducer = createReducer(
  initialState,
  on(CourseActions.loadCourses, state => ({ ...state, loading: true, error: null })),
  on(CourseActions.loadCoursesSuccess, (state, { courses }) => ({ ...state, courses, loading: false })),
  on(CourseActions.loadCoursesFailure, (state, { error }) => ({ ...state, error, loading: false })),
  on(CourseActions.enrollCourse, (state, { courseId }) => {
    if (state.enrolledIds.includes(courseId)) return state;
    return { ...state, enrolledIds: [...state.enrolledIds, courseId] };
  }),
  on(CourseActions.unenrollCourse, (state, { courseId }) => ({
    ...state,
    enrolledIds: state.enrolledIds.filter(id => id !== courseId)
  }))
);
