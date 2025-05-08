import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-borrowing-books',
  templateUrl: './borrowing-books.component.html',
  styleUrls: ['./borrowing-books.component.css']
})
export class BorrowingBooksComponent implements OnInit{

  bookData: any = []
  bookDataRoute = "getBooks"
  email :any;
  
  constructor(private service : ServicesService){
    const user = sessionStorage.getItem('username');
    if(user){
      const users = JSON.parse(user);
      this.email = users.email      
    }
  }
  returnForm = new FormGroup({
    lentByUser : new FormControl(''),
    currentlyBorrowedByUser : new FormControl('')
  })
  ngOnInit(): void {
    this.service.getBooks(this.bookDataRoute).subscribe((result:any)=>{
      result.forEach((object:any )=> {        
        if(object.currentlyBorrowedByUser==this.email){
          this.bookData.push(object);
          this.returnForm.controls['lentByUser'].setValue(object.lentByUser)
          this.returnForm.controls['currentlyBorrowedByUser'].setValue(this.email)
        }
      });
    })
  }
  returnBook(id:any){
    this.service.returnBook(id , this.returnForm.value).subscribe((result)=>{
      console.warn("Success",this.returnForm.value)
    })
    window.location.reload();
  }
}
