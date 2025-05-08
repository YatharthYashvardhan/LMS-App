import { Component, OnInit } from '@angular/core';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  isLoggedIn: any;
  email : any;
  token : any;
  route: any = "getBookToken"
  constructor(private service : ServicesService) {
    this.isLoggedIn = sessionStorage.getItem('username') != null;
    console.log("login", this.isLoggedIn);

    const username = sessionStorage.getItem('username');
    if (username) {
      const userData = JSON.parse(username);
      this.email = userData.email;
    }
  }
  ngOnInit(): void {
    this.service.getToken(this.email,this.route).subscribe((result)=>{
      this.token = result
    })
  }
  logout() {
    sessionStorage.clear();
    localStorage.removeItem('token');
    this.isLoggedIn = false;
    console.log(this.isLoggedIn);
  }
}
