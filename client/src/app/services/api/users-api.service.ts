import { inject, Injectable } from '@angular/core';
import { BaseApi } from './base-api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersApiService extends BaseApi {
  private usersUrl = this.baseUrl + 'users';
  private http = inject(HttpClient);

  constructor() {
    super();
  }
  
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl);
  }
}
