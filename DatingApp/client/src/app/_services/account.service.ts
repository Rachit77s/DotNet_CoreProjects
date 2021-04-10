import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = 'https://localhost:44347/api/';

  //Create a observable where we will store the current user, its like a buffer object ReplaySubject
  //Store only 1 previous value i.e. Store only one user
  private currentUserSource = new ReplaySubject<User>(1);
  
  //It is a observable
  currentUser$ = this.currentUserSource.asObservable();   

  constructor(private http : HttpClient) { 

  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response : User) => {
        const user = response;
        if(user) {
          //When the user login save it in local storage
          localStorage.setItem('user', JSON.stringify(user));
          //Set the current user
          this.currentUserSource.next(user);
        }

      })
    );
  }

  //Helper method
setCurrentUser(user: User) {
  this.currentUserSource.next(user);
}

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
