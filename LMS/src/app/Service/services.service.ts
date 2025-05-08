import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {
  urlaccount = "https://localhost:44358/api/Account"
  urlBooks = "https://localhost:44358/api/Books"

  bookData : any = [];
  constructor(private http : HttpClient) { }
  loginUser(data : any , signin:any){
    console.warn("Service",data)
    return this.http.post(`${this.urlaccount}/${signin}`,data)   
  }
  getToken(email: string, route: any) {
    console.warn('Email', email);
    return this.http.get(`${this.urlaccount}/${route}?email=${email}`);
  }  
  getBooks(route: any)
  {
    return this.http.get(`${this.urlBooks}/${route}`); 
  }
  addBook(data: any, route: any){
    console.warn("Add Data Value", data)
    return this.http.post(`${this.urlBooks}/${route}`, data);
  }
  borrowBook(id:any , data: any ){
    return this.http.put(`${this.urlBooks}/bookBorrow/${id}`, data);
  }
  returnBook(id:any , data: any ){
    return this.http.put(`${this.urlBooks}/returnBook/${id}`, data);
  }
}
