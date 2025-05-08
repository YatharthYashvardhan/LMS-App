import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Subscriber } from 'rxjs';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})
export class BooksListComponent implements OnInit{
  bookData: any = []
  filteredBookData: any[] = []; 
  bookDataRoute = "getBooks"
  email :any;
  isLoggedIn: any;
  @ViewChild('searchInput') searchInput!: ElementRef;

  constructor(private service : ServicesService){
    this.isLoggedIn = sessionStorage.getItem('username') != null;
    const user = sessionStorage.getItem('username');
    if(user){
      const users = JSON.parse(user);
      this.email = users.email      
    }
  }
  ngOnInit(): void {
    this.service.getBooks(this.bookDataRoute).subscribe((result:any)=>{
      console.warn("Book List", result)
      this.bookData = result
      this.filterBook();
    })
  }
  filterBook(){
    const searchTerm = this.searchInput.nativeElement.value.toLowerCase();
    if (searchTerm.trim() === '') {
      this.filteredBookData = [...this.bookData];
    } else {
      this.filteredBookData = this.bookData.filter((book:any) =>
        book.name.toLowerCase().includes(searchTerm) ||
        book.author.toLowerCase().includes(searchTerm) ||
        book.genre.toLowerCase().includes(searchTerm)
      );
    }
    console.warn(this.filteredBookData)

  }
}
