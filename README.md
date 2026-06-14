# PMS API - ASP.NET Core Backend

## Overview

PMS API is a backend project management and product management system built using ASP.NET Core Web API, PostgreSQL, and Entity Framework Core.

The project was built as a learning-focused backend system to understand real-world backend architecture concepts such as authentication, authorization, layered architecture, DTOs, Entity Framework Core, middleware, pagination, soft delete, auditing, and API design.

---

# Tech Stack

* ASP.NET Core Web API (.NET 9)
* PostgreSQL
* Entity Framework Core
* JWT Authentication
* BCrypt Password Hashing
* Swagger/OpenAPI

---

# Features

## Authentication

* User Registration
* User Login
* JWT Authentication
* Protected APIs
* Password Hashing using BCrypt

## Product Categories

* Create Category
* Update Category
* Get Category List
* Get Category Details
* Soft Delete Category
* Restore Category
* Pagination & Search

## Products

* Create Product
* Update Product
* Product Listing
* Product Details
* Assign Category
* Soft Delete Product
* Restore Product
* Pagination & Search

## Projects

* Create Project
* Update Project
* Project Listing
* Soft Delete Project
* Restore Project
* Pagination & Search

## Tasks

* Create Task
* Update Task
* Change Task Status
* Task Listing
* Soft Delete Task
* Restore Task
* Pagination & Search
* Orderable Task List

## Additional Features

* Global Query Filters
* Audit Fields
* Soft Delete Architecture
* Exception Middleware
* Dependency Injection
* DTO-based API Responses
* Entity Relationships
* Pagination & Sorting

---

# Architecture

The project follows layered backend architecture:

Request
→ Controller
→ Service Layer
→ Entity Framework Core
→ PostgreSQL Database

---

# Project Structure

```text id="m3d9pl"
PMS.Api
│
├── Controllers
├── Services
├── Interfaces
├── DTOs
├── Models
├── Data
├── Middleware
├── Migrations
├── Enums
└── Program.cs
```

---

# Database Features

## Soft Delete

Records are never permanently deleted.

Implemented using:

* IsDeleted
* DeletedAt
* DeletedBy

Global Query Filters automatically hide deleted records.

---

## Audit Fields

All main entities include:

* CreatedAt
* CreatedBy
* UpdatedAt
* UpdatedBy
* DeletedAt
* DeletedBy

---

# Authentication Flow

1. User registers or logs in
2. JWT token is generated
3. Token is sent in Authorization header
4. Protected APIs validate token using JWT middleware

Example:

```text id="k3p9ql"
Authorization: Bearer YOUR_TOKEN
```

---

# API Features

* RESTful APIs
* Proper HTTP Status Codes
* Consistent JSON Responses
* Pagination Support
* Search Support
* Global Error Handling

---

# Setup Instructions

## 1. Clone Repository

```bash id="r4t8wm"
git clone YOUR_REPOSITORY_URL
```

---

## 2. Navigate to Project

```bash id="n9s2dk"
cd PMSApi
```

---

## 3. Restore Packages

```bash id="v2x7qa"
dotnet restore
```

---

## 4. Configure Database

Update PostgreSQL connection string inside:

```text id="g8w1fd"
appsettings.json
```

Example:

```json id="p7q3zl"
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=PMSDb;Username=postgres;Password=yourpassword"
}
```

---

## 5. Run Migrations

```bash id="y5m8kt"
dotnet ef database update
```

---

## 6. Run Application

```bash id="u1r4bn"
dotnet run
```

---

# Swagger

Swagger UI is enabled in development mode.

Access Swagger at:

```text id="d6p2xm"
http://localhost:5064/swagger
```

---

# Concepts Learned

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL Integration
* JWT Authentication
* Authorization
* Dependency Injection
* Middleware
* DTOs
* Async/Await
* LINQ
* Relationships
* Pagination
* Soft Delete
* Auditing
* Exception Handling

---

# Future Improvements

* Refresh Tokens
* Role-Based Authorization
* Unit Testing
* Docker Support
* Activity Logs
* Notifications
* File Uploads
* AutoMapper
* FluentValidation

---

# Author

Prashant

Learning-focused backend development project built to understand real-world ASP.NET Core backend architecture and development workflow.
