import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminCareerComponent } from './ORS_Website/admin-career/admin-career.component';
import { ApplyCareerComponent } from './ORS_Website/apply-career/apply-career.component';
import { AdminBlogComponent } from './ORS_Website/admin-blog/admin-blog.component';
import { QuestionaryComponent } from './ORS_Website/questionary/questionary.component';
import { AboutComponent } from './ORS_Website/about/about.component';
import { BlogComponent } from './blog/blog.component';
import { CareerComponent } from './ORS_Website/career/career.component';
import { ContactComponent } from './ORS_Website/contact/contact.component';
import { HeaderComponent } from './ORS_Website/header/header.component';
import { OrsWebsiteComponent } from './ORS_Website/ors-website/ors-website.component';
import { PartnersTestimonialsComponent } from './ORS_Website/partners-testimonials/partners-testimonials.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminCareerComponent,
    ApplyCareerComponent,
    AdminBlogComponent,
    QuestionaryComponent,
    AboutComponent,
    BlogComponent,
    CareerComponent,
    ContactComponent,
    HeaderComponent,
    OrsWebsiteComponent,
    PartnersTestimonialsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
