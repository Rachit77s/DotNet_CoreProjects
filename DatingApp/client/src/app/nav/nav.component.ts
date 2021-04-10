import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}


  constructor(public accountService: AccountService) { }

  ngOnInit(): void {

  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
    }, error => {
    console.log(error);
    });
  }

  logout(){
    this.accountService.logout();
  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user; //This means if the user is null then set it to false else set to true
  //   }, error => {
  //     console.log(console.error);    
  //   })
  // }
}
