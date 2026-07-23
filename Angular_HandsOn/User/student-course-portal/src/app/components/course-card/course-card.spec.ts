import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CourseCard } from './course-card';
import { By } from '@angular/platform-browser';
import { CreditLabelPipe } from '../../pipes/credit-label-pipe';
import { Highlight } from '../../directives/highlight';
import { Course } from '../../models/course.model';

describe('CourseCard', () => {
  let component: CourseCard;
  let fixture: ComponentFixture<CourseCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseCard, CreditLabelPipe, Highlight]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CourseCard);
    component = fixture.componentInstance;
    component.course = { id: 1, name: 'Angular Testing', code: 'TEST101', credits: 3, gradeStatus: 'passed' };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the course name in an h3 tag', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h3')?.textContent).toContain('Angular Testing');
  });

  it('should emit enrollRequested event when enroll button is clicked', () => {
    spyOn(component.enrollRequested, 'emit');
    
    const buttons = fixture.debugElement.queryAll(By.css('button'));
    const enrollBtn = buttons[0];
    
    enrollBtn.triggerEventHandler('click', new Event('click'));
    
    expect(component.enrollRequested.emit).toHaveBeenCalledWith(1);
  });
});
