import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-partners-testimonials',
  templateUrl: './partners-testimonials.component.html',
  styleUrl: './partners-testimonials.component.css',
  standalone: true,
  imports: [CommonModule]
})

export class PartnersTestimonialsComponent {
  partners = [
    {
      name: 'Cisco',
      image: 'assets/logoImg/cisco.jpg',

    },
    {
      name: 'DellEmc',
      image: 'assets/partners/partner2.jpg',

    },
    {
      name: 'HP',
      image: 'assets/partners/partner3.jpg',

    },
    {
      name: 'Lenovo',
      image: 'assets/partners/partner4.jpg',
    },
    {
      name: 'MS',
      image: 'assets/partners/partner4.jpg',
    },
    {
      name: 'nvida',
      image: 'assets/partners/partner4.jpg',
    },
    // ...add more partners
  ];

  // Duplicate the list to create an infinite loop illusion
  duplicatedPartners = [...this.partners, ...this.partners];
}
