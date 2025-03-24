# 📚 Bookly.APIs

A **scalable & high-performance** RESTful API for managing a library system. This API efficiently handles user authentication, book management, borrowing, and returns while ensuring security, maintainability, and optimized performance.

## 🚀 Tech Stack & Architecture

- **ASP.NET Core Web API** – High-performance RESTful services
- **Onion Architecture** – Layered & modular for long-term maintainability
- **Entity Framework Core + SQL Server** – Reliable & efficient data storage
- **Specification Pattern** – Advanced filtering & query optimizations
- **Unit of Work & Repository Pattern** – Structured data handling & maintainability
- **JWT Authentication** – Secure user authentication & role management
- **Middleware for Error Handling & Logging** – Improves debugging & performance monitoring
- **AutoMapper** – Simplifies object mapping between layers
- **Fluent Validation** – Ensures robust request validation
- **Caching** – Enhances response speed & reduces database load
- **Rate Limiting** – Protects against abuse & ensures fair API usage

## ⚡ Core Features

✅ **User Authentication & Authorization** – Secure JWT-based login & role-based access
✅ **Book Management** – CRUD operations for books & categories
✅ **Borrowing & Returns System** – Track borrowed books with due dates
✅ **Advanced Search & Filtering** – Search by title, author, genre, availability
✅ **Global Exception Handling** – Structured error responses for better debugging
✅ **File Upload & Image Storage** – Manage book cover images
✅ **Pagination & Sorting** – Efficient data retrieval for large datasets
✅ **Admin Dashboard Features** – Manage books, users, and borrowing history
✅ **Performance Optimization** – Query optimizations, caching, & structured error handling

## 🛠️ Setup & Installation

### 1️⃣ Clone the repository
```sh
git clone https://github.com/0x0desha74/Library-Management-System.git
cd Library-Management-System
```

### 2️⃣ Configure the database
- Update the **appsettings.json** with your SQL Server connection string.
- Run the following command to apply database migrations:
```sh
dotnet ef database update
```

### 3️⃣ Run the application
```sh
dotnet run
```

### 4️⃣ API Documentation
- Swagger UI will be available at:
  ```
  http://localhost:<port>/swagger
  ```

## 🔒 Authentication & Security
- Uses **JWT-based authentication** for secure user access.
- **Role-based access control (RBAC)** for admin and regular users.
- **Global exception handling** to manage errors effectively.

## 🔥 Contributing
Pull requests are welcome! Feel free to fork the repo, create a branch, and submit a PR.

## 📜 License
This project is **open-source** under the [MIT License](LICENSE).

---
🚀 **Developed with ASP.NET Core & Clean Architecture** | Happy Coding! 😃
