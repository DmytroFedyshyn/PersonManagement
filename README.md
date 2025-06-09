# 👤 PersonManagement API (.NET 8)

This is a three-layered ASP.NET Core Web API project for managing people and their addresses. It follows clean separation of concerns: **Presentation (PL)**, **Business Logic (BLL)**, and **Data Access (DAL)** layers.

---

## 🛠️ Technologies Used

- ASP.NET Core 8
- Entity Framework Core 8 (Code-First)
- MSSQL Server
- AutoMapper
- FluentValidation
- xUnit + Moq + Faker
- Swagger / OpenAPI
- Clean Architecture principles

---

## 📐 Project Structure

```
PersonManagement.sln
├── PersonManagement.PL        // Presentation Layer (API)
├── PersonManagement.BLL       // Business Logic Layer
├── PersonManagement.DAL       // Data Access Layer
└── PersonManagement.Tests     // Unit Tests
```

---

## ✅ Features

- Add a new person (with optional address)
- Get all people with filtering by:
  - First name
  - Last name
  - City
- AutoMapper for DTO <-> Entity mapping
- Input validation with FluentValidation
- Global exception handling middleware
- Fully tested business logic and repository

---

## 📦 API Endpoints

Base URL: `https://localhost:{port}/api/person`

### ➕ POST `/api/person`

Create a new person

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "city": "Kyiv",
  "addressLine": "Shevchenka St"
}
```

### 📄 GET `/api/person`

Query people

Example:

```
/api/person?firstName=John&city=Kyiv
```

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/DmytroFedyshyn/PersonManagement.git
cd PersonManagement
```

---

### 2. Create and Apply EF Core Migrations (Required ❗)

This project uses Entity Framework Core (Code First). Migrations are **not included in the repository**, so you need to generate and apply them manually before running the API.

#### ✅ Step-by-step:

1. Make sure you have the EF Core CLI tools installed:

```bash
dotnet tool install --global dotnet-ef
```

2. From the root folder (where the `.sln` file is), generate the initial migration:

```bash
dotnet ef migrations add InitialCreate --project PersonManagement.DAL --startup-project PersonManagement.PL
```

3. Apply the migration to the database:

```bash
dotnet ef database update --project PersonManagement.DAL --startup-project PersonManagement.PL
```

> 💡 The connection string for SQL Server is configured in `appsettings.json` of the `PersonManagement.PL` project.

---

### 3. Run the API

```bash
dotnet run --project PersonManagement.PL
```

Swagger UI: [https://localhost:{port}/swagger](https://localhost:{port}/swagger)

---

## 🧪 Running Tests

```bash
dotnet test
```

Unit tests are written using `xUnit`, `Moq` and `Faker`. All dependencies are mocked — no real database used.

## 👨‍💻 Author

Created by [DmytroFedyshyn](https://github.com/DmytroFedyshyn) as a test project for .NET developer positions.

---
