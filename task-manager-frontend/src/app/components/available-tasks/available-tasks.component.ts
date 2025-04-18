export class AvailableTasksComponent {
  @Input() tasks: Task[] = [];
  @Output() assignTasks = new EventEmitter<number[]>();
  @Output() pageChange = new EventEmitter<number>();

  selectedTaskIds: number[] = [];
  currentPage = 1;
  pageSize = 10;

  // Add pagination methods
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.pageChange.emit(this.currentPage);
    }
  }

  nextPage(): void {
    this.currentPage++;
    this.pageChange.emit(this.currentPage);
  }

  // Existing methods remain the same
  toggleSelection(task: Task): void {
    const index = this.selectedTaskIds.indexOf(task.id);
    if (index !== -1) {
      this.selectedTaskIds.splice(index, 1);
    } else {
      this.selectedTaskIds.push(task.id);
    }
  }

  assignSelected(): void {
    if (this.selectedTaskIds.length > 0) {
      this.assignTasks.emit(this.selectedTaskIds);
      this.selectedTaskIds = [];
    }
  }
}