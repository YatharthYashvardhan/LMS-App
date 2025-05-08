import { Component ,OnInit} from '@angular/core';
import { FormGroup , FormControl, Validators} from '@angular/forms';
import { Router, RouterEvent } from '@angular/router';
import { ServicesService } from 'src/app/Service/services.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  SigninUser = new FormGroup({
    email : new FormControl('',[Validators.required]),
    Password : new FormControl('',[Validators.required])
  })
  route = "signin"
  collection : any = [];
  constructor(private login : ServicesService , private navrouter : Router){}
  ngOnInit(): void {
  }
  LoginUser(){ 
    console.warn(this.SigninUser.value)
    this.login.loginUser(this.SigninUser.value , this.route).subscribe((result : any)=>{
      this.collection = result;
      sessionStorage.setItem('username', JSON.stringify(this.collection));
      localStorage.setItem('token' , result.token);
      console.warn("result is here",result);  
      if(this.collection.email != null)
      {
       this.SigninUser.reset();
       setTimeout(() => {
        this.navrouter.navigate(['/']);
      }, 10);
      setTimeout(() => {
        location.reload();
      }, 10);
      }
    })
  }
  get Email() : FormControl {
    return this.SigninUser.get("email") as FormControl;
  }
  get Password() : FormControl{
    return this.SigninUser.get("Password") as FormControl;
  }
}
