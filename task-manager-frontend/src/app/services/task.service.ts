// src/app/services/task.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Task } from '../models/task.model';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private baseUrl = 'https://localhost:7281/api/tasks';

  constructor(private http: HttpClient) { }

  /**
   * Assigned tasks come from GET /api/tasks/user/{id}
   * Returns Task[] directly—no pagination wrapper here.
   */
  getAssignedTasks(userId: number): Observable<Task[]> {
    return this.http
      .get<Task[]>(`${this.baseUrl}/user/${userId}`);
  }

  /**
   * Available tasks: since there's no /available endpoint,
   * we fetch all (paginated) then filter out assigned ones.
   * GET /api/tasks?page=1&pageSize=100 (or whatever max you need)
   */
  getAvailableTasks(userId: number): Observable<Task[]> {
    const params = new HttpParams()
      .set('page', '1')
      .set('pageSize', '100');    // adjust pageSize if you need fewer

    return this.http
      .get<PaginatedResponse<Task>>(`${this.baseUrl}`, { params })
      .pipe(
        map(resp => resp.data.filter(t => t.assignedUserId !== userId))
      );
  }

  /**
   * Assign endpoint is POST /api/tasks/assign
   * It returns 200 + message on success, or 400 + message on failure.
   * We capture both cases and normalize to { isSuccess, message }.
   */
  assignTasks(userId: number, taskIds: number[]): Observable<{ isSuccess: boolean; message: string }> {
    return this.http
      .post<string>(`${this.baseUrl}/assign`, { userId, taskIds }, { observe: 'response' })
      .pipe(
        map((resp: HttpResponse<string>) => ({
          isSuccess: resp.status === 200,
          message: resp.body || 'OK'
        })),
        catchError(err =>
          of({
            isSuccess: false,
            message: err.error || err.statusText || 'Assignment failed'
          })
        )
      );
  }
}
