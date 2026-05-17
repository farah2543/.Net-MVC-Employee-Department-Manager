# .NET MVC Employee Department Manager

An ASP.NET MVC web application for managing employees and departments using the MVC architectural pattern.  
The project demonstrates CRUD operations, Entity Framework integration, relational database handling, and clean layered application structure.

---

## Features

- Employee Management
- Department Management
- Create, Read, Update, Delete (CRUD) Operations
- ASP.NET MVC Architecture
- Entity Framework Integration
- SQL Server Database Connectivity
- Form Validation
- Responsive UI
- Relationship Handling Between Employees & Departments

---

## Tech Stack

### Backend
- ASP.NET MVC
- C#
- Entity Framework
- LINQ

### Frontend
- HTML5
- CSS3
- Bootstrap
- Razor Views

### Database
- SQL Server

---

## Project Structure

```bash
├── Controllers/
├── Models/
├── Views/
│   ├── Employee/
│   ├── Department/
│   └── Shared/
├── Data/
├── wwwroot/
├── appsettings.json
└── Program.cs
```

---

## Getting Started

### Prerequisites

Before running the project, make sure you have installed:

- Visual Studio
- .NET SDK
- SQL Server
- SQL Server Management Studio (SSMS)

---

## Installation

Clone the repository:

```bash
git clone https://github.com/farah2543/.Net-MVC-Employee-Department-Manager.git
```

Navigate to the project directory:

```bash
cd .Net-MVC-Employee-Department-Manager
```

Open the solution file in Visual Studio.

---

## Database Setup

1. Open SQL Server.
2. Update the connection string in:

```json
appsettings.json
```

3. Apply migrations and update the database:

```bash
Update-Database
```

---

## Run the Application

Run the project using Visual Studio or execute:

```bash
dotnet run
```

The application will start locally on:

```bash
https://localhost:xxxx/
```

---

## Functionalities

### Employee Module
- Add new employees
- Edit employee information
- Delete employees
- View employee details
- Assign employees to departments

### Department Module
- Create departments
- Update department information
- Delete departments
- View department employees

---

## Learning Objectives

This project was built to practice:

- ASP.NET MVC Architecture
- Entity Framework CRUD Operations
- Database Relationships
- Razor View Engine
- Form Handling & Validation
- Clean Code Organization
- SQL Server Integration

---

## Future Improvements

- Authentication & Authorization
- Role-Based Access Control
- Search & Filtering
- Pagination
- Dashboard Analytics
- REST API Integration
- Profile Image Upload

---

## Screenshots

Add screenshots for:
- Employee List
- Department List
- Create/Edit Forms
- Details Pages

Example:

```markdown
![Employees Page](./screenshots/employees.png)
```

---

## Author

### Farah Mohamed

- GitHub: [farah2543](https://github.com/farah2543)
- Repository: [Employee Department Manager](https://github.com/farah2543/.Net-MVC-Employee-Department-Manager)

---

## License

This project is for educational and learning purposes.
