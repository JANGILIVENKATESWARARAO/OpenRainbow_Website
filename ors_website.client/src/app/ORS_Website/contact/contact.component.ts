import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule]
})

export class ContactComponent {
  form = {
    name: '',
    email: '',
    phone: '',
    message: ''
  };

  onSubmit() {
    if (this.form.name && this.form.email && this.form.phone && this.form.message) {
      console.log('Form Data:', this.form);
      // You can integrate service call here
      alert('Your message has been sent!');
    }
  }
}
