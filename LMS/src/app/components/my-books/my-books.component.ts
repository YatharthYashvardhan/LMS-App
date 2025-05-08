import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-my-books',
  templateUrl: './my-books.component.html',
  styleUrls: ['./my-books.component.css']
})
export class MyBooksComponent implements OnInit{

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
  ngOnInit(): void {
    this.service.getBooks(this.bookDataRoute).subscribe((result:any)=>{
      result.forEach((object:any )=> {        
        if(object.lentByUser==this.email){
          this.bookData.push(object);
        }
      });
    })
  }

}
