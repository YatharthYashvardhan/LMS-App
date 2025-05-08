import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule} from "@angular/forms";
import {FormsModule} from '@angular/forms'
import {HttpClientModule} from "@angular/common/http"
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { BooksListComponent } from './components/books-list/books-list.component';
import { ServicesService } from './Service/services.service';
import { DatePipe } from '@angular/common';
import { AddBookComponent } from './components/add-book/add-book.component';
import { ViewBookComponent } from './components/view-book/view-book.component';
import { BorrowingBooksComponent } from './components/borrowing-books/borrowing-books.component';
import { MyBooksComponent } from './components/my-books/my-books.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    BooksListComponent,
    AddBookComponent,
    ViewBookComponent,
    BorrowingBooksComponent,
    MyBooksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    ServicesService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
