import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { Task } from '../../models/task.model';
import { UserService } from '../../services/user.service';
import { TaskService } from '../../services/task.service';
import { PaginatedResponse } from '../../models/paginated-response';

@Component({
  selector: 'app-task-assignment',
  templateUrl: './task-assignment.component.html',
  styleUrls: ['./task-assignment.component.css']
})
export class TaskAssignmentComponent implements OnInit {

  onAssignTasks(taskIds: number[]): void {
    if (!this.selectedUser) {
      return;
    }
    
    this.taskService.assignTasks(this.selectedUser.id, taskIds).subscribe({
      next: () => {
        this.onUserSelected(this.selectedUser!);
      },
      error: (error) => {
        console.error('Error assigning tasks:', error);
        alert('Failed to assign tasks. Please try again later.');
      }
    });
  }
  
  users: User[] = [];
  selectedUser?: User;
  assignedTasks: Task[] = [];
  availableTasks: Task[] = [];
  currentPage = 1;
  pageSize = 10;
  totalPages = 0;

  constructor(
    private userService: UserService,
    private taskService: TaskService
  ) {}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  loadAvailableTasks() {
    if (!this.selectedUser) return;
    
    this.taskService.getAvailableTasks(this.selectedUser.id, this.currentPage, this.pageSize)
      .subscribe({
        next: (response: PaginatedResponse<Task>) => {
          this.availableTasks = response.data;
          this.totalPages = response.totalPages;
          this.currentPage = response.page;
        },
        error: (err: any) => {
          console.error('Failed to load available tasks', err);
        }
      });
  }

  onUserSelected(user: User): void {
    this.selectedUser = user;
    this.currentPage = 1; // Reset to first page when user changes
    this.loadAvailableTasks();
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
    this.loadAvailableTasks();
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.onPageChange(this.currentPage + 1);
    }
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.onPageChange(this.currentPage - 1);
    }
  }
}