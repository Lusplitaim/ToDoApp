import { inject, Injectable } from '@angular/core';
import { UsersApiService } from './api/users-api.service';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private usersApi = inject(UsersApiService);

  getUsers(): Observable<User[]> {
    return this.usersApi.getUsers();
  }
}
