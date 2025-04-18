import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7281/api/users';

  constructor(private http: HttpClient) { }

  /**
   * Fetch the full list of users from the backend
   */
  getUsers(): Observable<User[]> {
    var response = this.http.get<User[]>(this.baseUrl);
    console.log('response', response);
    return response;
  }
}
