# Library Management API

## About the Project

Library Management API is an ASP.NET Core Web API created to manage the main operations of a library. It allows books, authors, members, and loans to be created, viewed, updated, and deleted.

The main purpose of the project is to practise building a maintainable backend application with a layered architecture. Instead of placing all logic inside controllers, responsibilities are separated between controllers, services, repositories, and the database layer.

## Technologies and Why They Are Used

- **ASP.NET Core Web API** is used to create RESTful endpoints and return appropriate HTTP responses.
- **Entity Framework Core** is used as an ORM to communicate with the database through C# entities and LINQ queries.
- **SQL Server** is used to store books, authors, members, loans, and their relationships.
- **Code First migrations** are used to create and update the database structure from the entity models.
- **Swagger/OpenAPI** is used to document and test the API endpoints from the browser.
- **xUnit and Moq** are used to test the service layer without connecting to the real database.

## Architecture

The solution is divided into API, Application, Core, Infrastructure, and Tests projects.

- **API** contains controllers, middleware, and application configuration.
- **Application** is separated for application-level components such as validators and future use cases.
- **Core** contains entities, DTOs, interfaces, and common classes.
- **Infrastructure** contains `AppDbContext`, repositories, services, and migrations.
- **Tests** contains unit tests for service behavior.

The main request flow is:

```text
Controller -> Service -> Repository -> AppDbContext -> SQL Server
```

Controllers only communicate with service interfaces, while services communicate with repository interfaces. The implementations are registered with dependency injection using `AddScoped`.

## Entities and Relationships

The project contains four main entities: `Book`, `Author`, `Member`, and `Loan`.

- A book can have multiple authors, and an author can have multiple books. This is a many-to-many relationship.
- A book can have multiple loans. This is a one-to-many relationship.
- A member can have multiple loans. This is also a one-to-many relationship.

These relationships and unique indexes are configured with the EF Core Fluent API. `BookCode` and member email values are unique.

## DTO Usage

Create, update, and response DTOs are used so that entities are not returned directly from the API. This keeps the database models separate from request and response models. Entity-to-DTO mapping is performed manually in the service layer.

## Main Features

- Full GET, POST, PUT, and DELETE operations for books, authors, members, and loans
- Correct HTTP status codes such as `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `404 Not Found`, and `409 Conflict`
- Input validation with Data Annotations
- Unique book code and member email checks
- Validation of book copy counts, active members, loan dates, and related records
- Centralized exception handling with `ExceptionMiddleware`
- Pagination and sorting for the book list
- Swagger documentation and endpoint testing

The book list supports `pageNumber`, `pageSize`, `sortBy`, and `sortDirection` query parameters. For example:

```http
GET /api/books?pageNumber=2&pageSize=5&sortBy=title&sortDirection=desc
```

## Validation and Error Handling

Data Annotations such as `Required`, `MaxLength`, `EmailAddress`, and `Range` are used to validate incoming data. The `ApiController` attribute automatically returns `400 Bad Request` when a request model is invalid.

`ExceptionMiddleware` handles exceptions in one central place. It returns consistent responses for invalid input, missing records, duplicate values, and unexpected server errors. This keeps repeated `try-catch` blocks out of the controllers.

## Running the Project

1. Add your SQL Server connection string as `DefaultConnection` in `appsettings.json`.
2. Apply the migrations from Visual Studio Package Manager Console:

```powershell
Update-Database -Project LibraryManagement.Infrastructure -StartupProject LibraryManagement
```

3. Set `LibraryManagement` as the startup project and run the application.
4. Open `/swagger` to view and test the endpoints.

## Testing

The `LibraryManagement.Tests` project contains a unit test for `BookService`. `IBookRepository` is mocked with Moq so the service can be tested independently from Entity Framework Core and SQL Server.

The test verifies that `GetByIdAsync` returns the expected book data when the requested book exists. The test was executed successfully through Visual Studio Test Explorer.
