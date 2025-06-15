# 🚀 SleekLoan - Blazor Server Loan Application System

This is a Blazor Server application built with .NET 9, Entity Framework Core, and clean architecture. It allows users to create, view, update, and delete loan applications with full CRUD support.

---

## 🛠️ Technologies Used

- [.NET 9](https://dotnet.microsoft.com/)
- Blazor Server
- Entity Framework Core
- Postgres Sql (Database is already provisioned)
- Dependency Injection
- LINQ, async/await
- Clean architecture

---

## 📦 Project Structure

SleekLoan.sln
│
├── SleekLoan.Server/ # Blazor Server App (UI + DI setup)
├── SleekLoan.Domain/ # Entity Models and Enums
├── SleekLoan.Application/ # DTOs, Interfaces, and Services
├── SleekLoan.Infrastructure/ # EF Core, DbContext, Repositories

# Navigate to the Infrastructure project
cd SleekLoan.Infrastructure

# Add a migration (if needed)
dotnet ef migrations add InitialCreate --startup-project ../SleekLoan.Infrastructure

# Apply the migration to the database
dotnet ef database update --startup-project ../SleekLoan.Infrastructure

# Run the application
dotnet run --project SleekLoan.Server

