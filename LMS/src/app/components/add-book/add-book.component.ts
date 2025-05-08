import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit{

  addBook = new FormGroup({
    name: new FormControl('', [Validators.required]),
    author: new FormControl('', [Validators.required]),
    rating: new FormControl('', [Validators.required, Validators.min(0), Validators.max(5),Validators.pattern(/^\d+(\.\d)?$/)]),
    description: new FormControl('', [Validators.required]),
    genre: new FormControl('', [Validators.required]),
    lentByUser: new FormControl(''),
    currentlyBorrowedByUser: new FormControl('')
  });

  email :any;
  addBookRoute = "addBooks"
  bookCollection :any = [];

  constructor(private service : ServicesService , private router : Router){
    const user = sessionStorage.getItem('username');
    if(user){
      const users = JSON.parse(user);
      this.email = users.email      
    }
    this.addBook.controls['lentByUser'].setValue(this.email);
  }

  ngOnInit(): void {
    console.warn(this.email)
  }

  Submit(){
    console.warn(this.addBook.value)
    if(this.addBook.valid){
      console.log("Valid Data")
      this.service.addBook(this.addBook.value, this.addBookRoute).subscribe((result)=>{
        this.bookCollection = result
      })
      this.addBook.reset();
      this.router.navigate(['/'])
    }
    else{
      console.log("Invalid Data")
      this.addBook.markAllAsTouched();
    }
  }
}
