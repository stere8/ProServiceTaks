

// src/app/models/paginated-response.ts
export interface PaginatedResponse<T> {
    data: T[];
    page: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
  }
  