import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-career',
  templateUrl: './career.component.html',
  styleUrl: './career.component.css',
  standalone: true,
  imports: [CommonModule]
})

export class CareerComponent {
  // Sample static data; in a real case, this would come from the admin form or an API
  jobs = [
    {
      jobId: '001',
      jobRole: 'Software Engineer',
      jobDesc: 'Develop and maintain web applications.',
      noOfOpenings: 5,
      experience: '2-5 years',
      skills: 'JavaScript, React, Node.js',
      endDate: '2025-06-30',
      applyLink: ''
    },
    {
      jobId: '002',
      jobRole: 'UI/UX Designer',
      jobDesc: 'Design user interfaces and improve user experiences.',
      noOfOpenings: 3,
      experience: '3-6 years',
      skills: 'Figma, Sketch, Adobe XD',
      endDate: '2025-06-15',
      applyLink: ''
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }
}
