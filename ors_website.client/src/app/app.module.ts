import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminCareerComponent } from './ORS_Website/admin-career/admin-career.component';
import { ApplyCareerComponent } from './ORS_Website/apply-career/apply-career.component';
import { AdminBlogComponent } from './ORS_Website/admin-blog/admin-blog.component';
import { QuestionaryComponent } from './ORS_Website/questionary/questionary.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminCareerComponent,
    ApplyCareerComponent,
    AdminBlogComponent,
    QuestionaryComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
