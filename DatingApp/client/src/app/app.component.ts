import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit  {
  title = 'The Dating App';
  

  constructor( private accountService: AccountService) {};

  ngOnInit() {
    // this.getUsers();
    this.setCurrentUser();
  }

  //Check the current user
setCurrentUser(){
  //Get the current user if present
  const user: User = JSON.parse(localStorage.getItem('user'));
  //
  this.accountService.setCurrentUser(user);
}

// getUsers() {
//   this.http.get('https://localhost:44347/api/users').subscribe(respone => {
//     this.users = respone;
//   }, error => {
//     console.log(error);
//   })

// }

}
