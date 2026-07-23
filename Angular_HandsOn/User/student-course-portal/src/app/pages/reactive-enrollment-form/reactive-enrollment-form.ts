import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, FormArray } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

export function noCourseCode(control: AbstractControl): ValidationErrors | null {
  if (control.value && control.value.toString().startsWith('XX')) {
    return { noCourseCode: true };
  }
  return null;
}

export function simulateEmailCheck(control: AbstractControl): Promise<ValidationErrors | null> {
  return new Promise(resolve => {
    setTimeout(() => {
      if (control.value && control.value.includes('test@')) {
        resolve({ emailTaken: true });
      } else {
        resolve(null);
      }
    }, 800);
  });
}

@Component({
  selector: 'app-reactive-enrollment-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reactive-enrollment-form.html',
  styleUrl: './reactive-enrollment-form.css',
})
export class ReactiveEnrollmentForm implements OnInit {
  enrollForm!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.enrollForm = this.fb.group({
      studentName: ['', [Validators.required, Validators.minLength(3)]],
      studentEmail: this.fb.control('', {
        validators: [Validators.required, Validators.email],
        asyncValidators: [simulateEmailCheck]
      }),
      courseId: [null, [Validators.required, noCourseCode]],
      preferredSemester: ['Odd', Validators.required],
      agreeToTerms: [false, Validators.requiredTrue],
      additionalCourses: this.fb.array([])
    });
  }

  get additionalCourses() {
    // This getter provides a typed FormArray to the template, avoiding casting in the template.
    return this.enrollForm.get('additionalCourses') as FormArray;
  }

  addCourse() {
    this.additionalCourses.push(this.fb.control('', Validators.required));
  }

  removeCourse(index: number) {
    this.additionalCourses.removeAt(index);
  }

  onSubmit() {
    console.log('Form Value:', this.enrollForm.value);
    // getRawValue includes disabled controls, whereas value only includes enabled ones
    console.log('Raw Value:', this.enrollForm.getRawValue());
    this.enrollForm.markAsPristine();
  }

  canDeactivate(): boolean {
    if (this.enrollForm.dirty) {
      return window.confirm('You have unsaved changes. Leave?');
    }
    return true;
  }
}
