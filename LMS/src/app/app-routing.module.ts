import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { BooksListComponent } from './components/books-list/books-list.component';
import { AddBookComponent } from './components/add-book/add-book.component';
import { ViewBookComponent } from './components/view-book/view-book.component';
import { BorrowingBooksComponent } from './components/borrowing-books/borrowing-books.component';
import { MyBooksComponent } from './components/my-books/my-books.component';

const routes: Routes = [
  {
    component: HomeComponent,
    path: "Home"
  },
  {
    component: LoginComponent,
    path: "Login"
  },
  {
    component : BooksListComponent,
    path: ""
  },
  {
    component: AddBookComponent,
    path :"AddBook"
  },
  {
    component: ViewBookComponent,
    path: "ViewBook/:id"
  },
  {
    component: BorrowingBooksComponent,
    path: "Borrowings"
  },
  {
    component: MyBooksComponent,
    path: "MyBooks"
  }



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
