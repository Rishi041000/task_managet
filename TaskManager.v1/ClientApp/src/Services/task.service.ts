import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Tasks } from '../../Models/models';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  createTask(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks/Create', request)
  }

  updateTask(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks/Update', request)
  }

  deleteTask(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks/Delete', request)
  }


  getAllTasks(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks', request)
  }

  getTaskById(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks/id', request)
  }

  getTaskBycriteria(request: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'tasks/search', request)
  }
}


