import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import countryCodesData from '../../common/assets/countryCodes.json';

declare var intlTelInput: any;

@Component({
  selector: 'app-apply-career',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './apply-career.component.html',
  styleUrl: './apply-career.component.css'
})

export class ApplyCareerComponent {
  applyForm: FormGroup;
  resumeFile: File | null = null;
  showPopup = false;
  isChecked = false;
  experienceOptions = Array.from({ length: 21 }, (_, i) => i); // [0, 1, 2, ..., 20]
  countryCodes = countryCodesData;
  filteredCountries = [...countryCodesData];
  countrySearch = '';
  showCountryDropdown = false;

  constructor(private fb: FormBuilder) {
    this.applyForm = this.fb.group({
      jobTitle: [{ value: 'Software Engineer', disabled: true }],
      jobId: [{ value: 'JOB123', disabled: true }],
      fullName: [''],
      phone: [''],
      countryCode: ['+'],
      email: [''],
      experience: [''],
      prevOrg: [''],
      prevOrgLoc: [''],
      expectedCtc: [''],
      currentCtc: [''],
      whyJoinInNewCompany: [''],
      terms: [false, Validators.requiredTrue]
    });
  }

  termsAccepted = false;

  acceptTerms() {
    this.termsAccepted = true;
    this.applyForm.get('terms')?.setValue(true); // ✅ set checkbox as checked
    this.showPopup = false;
  }

  //country code drop down 
  iti: any;

  ngAfterViewInit(): void {
    const input = document.querySelector('#phone') as HTMLInputElement;

    this.iti = intlTelInput(input, {
      initialCountry: 'in',
      separateDialCode: true,
      utilsScript: 'assets/utils.js',
    });

    input.addEventListener('blur', () => {
      const fullPhone = this.iti.getNumber();
      this.applyForm.patchValue({ phone: fullPhone });
    });
  }

  onSubmit() {
    if (this.applyForm.valid) {
      const formData = new FormData();
      Object.entries(this.applyForm.getRawValue()).forEach(([key, value]) => {
        formData.append(key, value as string);
      });

      if (this.resumeFile) {
        formData.append('resume', this.resumeFile);
      }

      formData.forEach((value, key) => {
        console.log(`${key}:`, value);
      });
    }
  }
}
