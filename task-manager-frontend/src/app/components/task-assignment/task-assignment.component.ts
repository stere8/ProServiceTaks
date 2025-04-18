// src/app/components/task-assignment/task-assignment.component.ts
import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { Task } from '../../models/task.model';
import { UserService } from '../../services/user.service';
import { TaskService } from '../../services/task.service';

@Component({
  selector: 'app-task-assignment',
  templateUrl: './task-assignment.component.html',
  styleUrls: ['./task-assignment.component.css']
})
export class TaskAssignmentComponent implements OnInit {
  users: User[] = [];
  selectedUser?: User;
  assignedTasks: Task[] = [];
  availableTasks: Task[] = [];

  constructor(
    private userService: UserService,
    private taskService: TaskService
  ) {}

  ngOnInit(): void {
    this.userService.getUsers()
      .subscribe({
        next: users => {
          console.log('fetched users:', users);
          this.users = users;
        },
        error: err => {
          console.error('getUsers failed', err);
        }
      });
  }
  
  onUserSelected(user: User): void {
    this.selectedUser = user;

    // must pass user.id and subscribe
    this.taskService.getAssignedTasks(user.id)
      .subscribe({
        next : tasks => {
        console.log('fetched tasks:', tasks);
        this.assignedTasks = tasks
          .sort((a,b) => b.difficulty - a.difficulty)
          .slice(0, 10);
      },
        error: err => {
          console.error('getAssignedTasks failed', err);
        }
      });

    this.taskService.getAvailableTasks(user.id)
      .subscribe({
        next : tasks => {
          console.log('available tasks:', tasks);
          this.availableTasks = tasks
            .sort((a,b) => b.difficulty - a.difficulty)
            .slice(0, 10);
        },
        error: err => {
          console.error('getAvailableTasks failed', err);
        }
      });
  }

  onAssignTasks(taskIds: number[]): void {
    if (!this.selectedUser) return;

    // subscribe to the POST Observable
    this.taskService.assignTasks(this.selectedUser.id, taskIds)
      .subscribe(response => {
        if (response.isSuccess) {
          // refresh lists after successful assignment
          this.onUserSelected(this.selectedUser!);
        } else {
          alert('Assignment failed: ' + response.message);
        }
      }, err => {
        console.error('HTTP error', err);
        alert('Server error during assignment');
      });
  }
}
