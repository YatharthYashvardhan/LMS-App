import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.css']
})
export class ViewBookComponent implements OnInit{

  bookData : any = [];
  bookRoute = "getBooks"
  bookId: any
  emailLogin: any
  emailMatch:any
  bookToken:any
  route:any = "getBookToken"

  constructor(private service : ServicesService , private router : Router , private activatedRoute : ActivatedRoute){
    this.bookId = activatedRoute.snapshot.params['id'];
    console.warn("This",this.bookData)

    const user = sessionStorage.getItem('username');
    if(user){
      const users = JSON.parse(user);
      this.emailLogin = users.email      
    }
    console.warn(this.bookData.name)
  }

  borrowForm = new FormGroup({
    lentByUser : new FormControl(''),
    currentlyBorrowedByUser : new FormControl('')
  })

  ngOnInit(): void {
    console.warn(this.bookId)
    this.service.getBooks(this.bookRoute).subscribe((result:any)=>{
      result.forEach((object:any )=> {        
        if(object.book_Id==this.bookId){
          this.bookData.push(object);
          this.borrowForm.controls['lentByUser'].setValue(object.lentByUser)
          this.borrowForm.controls['currentlyBorrowedByUser'].setValue(this.emailLogin)
          if(this.emailLogin == object.lentByUser){
            this.emailMatch = true
          }
        }
      });
    })
    this.service.getToken(this.emailLogin,this.route).subscribe((result)=>{
      this.bookToken = result
    })
  }
  borrowBook(id : any){
    console.warn(this.borrowForm.value)
    this.service.borrowBook(id , this.borrowForm.value).subscribe((result)=>{
      console.warn("Success",this.borrowForm.value)
    })
    window.location.reload();
  }
}

