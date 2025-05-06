import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.css',
  imports: [CommonModule],
  standalone: true
})

export class BlogComponent {
  tabs = ['All', 'News', 'Events', 'Videos', 'Articles'];

  selectedTab = 'All';

  blogCards = [
    { title: '350% Growth', category: 'News', color: 'orange' },
    { title: '95% ROAS', category: 'Articles', color: 'white' },
    { title: '+90k Google Views', category: 'Events', color: 'pink' },
    { title: 'Monthly Stats', category: 'Videos', color: 'green' },
    { title: 'Revenue Last Quarter', category: 'News', color: 'purple' },
    { title: 'Customer Loyalty Boost', category: 'Articles', color: 'white' }
  ];

  get filteredCards() {
    return this.selectedTab === 'All'
      ? this.blogCards
      : this.blogCards.filter(card => card.category === this.selectedTab);
  }

  selectTab(tab: string) {
    this.selectedTab = tab;
  }
}
