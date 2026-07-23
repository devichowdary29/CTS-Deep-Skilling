import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CourseService } from './course';

describe('CourseService', () => {
  let service: CourseService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CourseService]
    });
    service = TestBed.inject(CourseService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should retrieve courses via GET', () => {
    const dummyCourses = [
      { id: 1, name: 'Angular Basics', code: 'ANG101', credits: 3, gradeStatus: 'passed' as const }
    ];

    service.getCourses().subscribe(courses => {
      expect(courses.length).toBe(1);
      expect(courses).toEqual(dummyCourses);
    });

    const req = httpMock.expectOne('http://localhost:3000/courses');
    expect(req.request.method).toBe('GET');
    req.flush(dummyCourses);
  });
});
