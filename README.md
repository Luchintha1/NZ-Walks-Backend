# NZ-Walks
## What's this about?
A backend RESTful API built with ASP.NET Core 8.0 to manage and store data about walking trails and regions in New Zealand.

## Features

- Manage Regions and Walks with full CRUD operations

- Built using Entity Framework Core with SQL Server as the database

- Implements Repository Pattern for clean architecture

- API Documentation with Swagger / OpenAPI

- JWT Authentication & Authorization (role-based access)

- DTOs and AutoMapper for efficient data handling

- Exception handling and validation

## Tech Stack

- .NET 8.0 (ASP.NET Core Web API)

- Entity Framework Core

- SQL Server

- Swagger / OpenAPI

- JWT Authentication

- AutoMapper

## Getting Started

### 1) Clone this repository

git clone https://github.com/your-username/NZ-Walks.git


### 2) Update the connection string in appsettings.json.

### 3) Run database migrations:

dotnet ef database update


### 4) Launch the API:

dotnet run

### 5) Open https://localhost:5001/swagger to explore the API.

## Project Structure
NZWalks.API/
 ├── Controllers/         # API Controllers
 ├── Models/              # Domain Models
 ├── DTOs/                # Data Transfer Objects
 ├── Repositories/        # Repository Interfaces & Implementations
 ├── Data/                # Database Context
 └── Program.cs           # Entry point

## Learning Goals

* This project was created to:
* Learn modern .NET 8 API development
* Practice clean architecture and repository pattern
* Understand JWT authentication in Web APIs
* Get hands-on experience with database migrations and EF Core
