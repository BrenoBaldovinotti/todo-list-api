# Todo List API & Frontend

## Project Description

This project is an interactive To-Do List application built using the .NET technology stack. It showcases scalable, maintainable, and efficient application development with both an API and frontend. The project demonstrates expertise in applying SOLID principles, clean architecture, and proper API management.

It consists of:
- A back-end API for managing tasks (CRUD operations).
- A Razor-based frontend for interacting with the task management system.

---

## Features and Requirements

### Implemented Features:

- [x] **Framework**: .NET 8.
- [x] **Frontend**: Implemented using MVC with Razor pages.
- [x] **Back-End API**: Handles the following CRUD operations for tasks:
   - Add a task
   - List all tasks
   - Mark a task as completed
   - Remove completed tasks
   - Edit task names (Bonus feature)
- [~] **Database**: PostgreSQL database was chosen over an in-memory database due to its reliability and SQL support. A local SQL Server was avoided because it’s more complex and costly to configure, and in-memory databases are less efficient for real-world scenarios.
- [x] **Authentication**: JWT-based authentication system.
- [x] **SOLID Principles**: Clean and maintainable structure following SOLID principles.
- [x] **ORM**: Entity Framework for database interactions.
- [x] **Unit Testing**: Implemented unit tests for services using xUnit.
- [ ] **Docker**: Not implemented yet, but the application can be containerized in the future.

---

## Setup Instructions

### Prerequisites
1. .NET SDK 8.0+
2. PostgreSQL installed locally.
3. Postman or any other API client (for testing API endpoints).

### Setting Up Locally

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/BrenoBaldovinotti/todo-list-api.git
   cd todo-list-api

2. **Setup Database**:
   ```json
    "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=todo_list;Username=your_username;Password=your_password"
    }
    ```

 3. **Run Migrations**: Navigate to the API folder and run:
    ```bash
    dotnet ef database update

 4. **Run API and Frontend Projects Simultaneously**: Use the existing launch profiles:
    ```bash
    dotnet run --project src/API
    dotnet run --project src/Frontend
    
 5. **Access the Application**:
    - API: http://localhost:5224/swagger
    - Frontend: http://localhost:5284/Login

## Architecture and Design Decisions

### Hexagonal Architecture
The project follows a clean architecture, separating concerns across distinct layers:

- **Application Layer**: Handles business logic.
- **Domain Layer**: Contains core entities and domain-specific rules.
- **Infrastructure Layer**: Manages database interaction (via Entity Framework) and external services.

### SOLID Principles

- **Single Responsibility**: Each service or class has a single, well-defined responsibility.
- **Open/Closed Principle**: Components are open for extension but closed for modification.
- **Dependency Injection**: Services and repositories are injected to ensure flexibility and testability.

### JWT Authentication
To secure API endpoints, a JWT-based authentication system is implemented, providing different roles (Admin, Operator) with varied access permissions.

### Testing
**xUnit** is used for unit testing the services and controllers, ensuring that business logic is well-tested.

---

## Conclusions
This Todo List application fulfills all the requested functionality and demonstrates a well-organized codebase using modern development practices. The application is designed for maintainability, scalability, and performance, with the bonus features providing additional flexibility and improved user experience.

