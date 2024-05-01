import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  login(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'user/Login',request)
  }

  logout(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'user/Logout', request)
  }
}
