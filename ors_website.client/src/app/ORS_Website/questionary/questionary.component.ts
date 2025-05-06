import { CommonModule } from '@angular/common';
import { AfterViewInit, Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

declare var intlTelInput: any;

@Component({
  selector: 'app-questionary',
  templateUrl: './questionary.component.html',
  styleUrl: './questionary.component.css',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})

export class QuestionaryComponent {
  applyForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.applyForm = this.fb.group({
      fullName: [''],
      phone: [''],
      countryCode: [''],
      email: [''],
      query: [''],
    });
  }

  //country code drop down 
  iti: any;

  ngAfterViewInit(): void {
    setTimeout(() => {
      const input = document.querySelector('#query-phone') as HTMLInputElement;
      if (!input) return;

      this.iti = intlTelInput(input, {
        initialCountry: 'in',
        separateDialCode: true,
        utilsScript: 'assets/utils.js',
      });

      input.addEventListener('blur', () => {
        const fullPhone = this.iti.getNumber();
        this.applyForm.patchValue({ phone: fullPhone });
      });
    });
  }
  
  onSubmit() {
    if (this.applyForm.valid) {
      const formData = new FormData();
      Object.entries(this.applyForm.getRawValue()).forEach(([key, value]) => {
        formData.append(key, value as string);
      });

      formData.forEach((value, key) => {
        console.log(`${key}:`, value);
      });

    }
  }
}
