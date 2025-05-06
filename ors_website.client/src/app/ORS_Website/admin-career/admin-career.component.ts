import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-admin-career',
  templateUrl: './admin-career.component.html',
  styleUrl: './admin-career.component.css',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule]
})

export class AdminCareerComponent {
  applyForm: FormGroup;
  experienceOptions = Array.from({ length: 21 }, (_, i) => i); // [0, 1, 2, ..., 20]
  skillOptions: string[] = [
    // Technical Skills
    'Java', 'Python', 'C#', 'JavaScript', 'Angular', 'React', 'Node.js', 'SQL', 'MongoDB', 'DevOps',
    'AWS', 'Azure', 'Docker', 'Kubernetes', 'HTML', 'CSS', 'TypeScript',
  
    // Analytical & Business Skills
    'Data Analysis', 'Business Intelligence', 'Excel', 'Power BI', 'Tableau',
    'Financial Modeling', 'Market Research', 'SEO', 'Digital Marketing',
  
    // Creative Skills
    'UI/UX Design', 'Graphic Design', 'Adobe Photoshop', 'Canva', 'Video Editing',
    'Content Writing', 'Copywriting', 'Photography',
  
    // Soft Skills
    'Communication', 'Teamwork', 'Leadership', 'Problem Solving', 'Time Management',
    'Critical Thinking', 'Adaptability', 'Emotional Intelligence', 'Conflict Resolution',
  
    // Management & Operational Skills
    'Project Management', 'Agile', 'Scrum', 'Customer Support', 'Sales', 'CRM Tools',
    'Operations Management', 'Quality Assurance', 'Procurement',
  
    // Languages (Optional)
    'English', 'Hindi', 'French', 'Spanish', 'German', 'Tamil', 'Telugu'
  ];
  selectedSkills: string[] = [];
  
  constructor(private fb: FormBuilder) {
    this.applyForm = this.fb.group({
      jobId: [''],
      jobTitle:  [''],
      jobDescription:  [''],
      openPositionsNo: [''],
      skills: [[],], 
      startDate:[''],
      endDate:[''],
      experience:['']
      
    });
  }
 
  onSkillsChange(event: any): void {
    const selectedOptions = event.target.selectedOptions;
    const selected: string[] = [];
  
    for (let i = 0; i < selectedOptions.length; i++) {
      const option = selectedOptions[i];
      if (option.selected && option.value) {
        selected.push(option.value);
      }
    }
  
    // Avoid duplicate selections
    const uniqueSelected = Array.from(new Set(selected));
    this.selectedSkills = uniqueSelected;
    this.applyForm.get('skills')?.setValue(uniqueSelected);
  }  
 
  removeSkill(skill: string): void {
    this.selectedSkills = this.selectedSkills.filter(s => s !== skill);
    this.applyForm.get('skills')?.setValue(this.selectedSkills);
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
