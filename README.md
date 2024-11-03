![Build Status](https://github.com/ngdatdev/BookShop-Api/actions/workflows/main.yml/badge.svg)

## 📝 Description

This project is a modern web API for managing a bookstore, built with .NET 8 and adhering to clean architecture principles. While it primarily implements CRUD operations, it incorporates various design patterns and best practices to ensure a scalable and maintainable application architecture.

## 🏗 Architecture

This project follows Vertiacl Slide Architecture(organizes the code around features), with clear separation of concerns:

- **Domain Layer**: Contains business entities and interfaces
- **Infrastructure Layer**: Implements data access and external services
- **Application Layer**: Contains business logic and use cases
- **Presentation Layer**: API controllers

<div style="text-align: center; margin: 0 50px;">
    <img src="https://res.cloudinary.com/diq0gyetg/image/upload/v1730646953/vertical-slice-architecture_tx5e8z.png" alt="Architecture diagram" style="max-width: 100%; height: auto;">
</div>


## 🚀 Key Features

- RESTful API design
- Advanced caching with Redis
- Database persistence with PostgreSQL
- Entity Framework Core for data access
- CQRS with MediatR pattern
- Unit testing and architecture testing (demo)
- Distributed caching support
- xUnit, AutoFixture and FluentAssertions for testing

## 🛠 Technologies & Tools

- ASP.NET Web API (.NET 8)
- PostgreSQL
- Entity Framework Core
- Redis Cache
- xUnit/AutoFixture/FluentAssertions
- PayOS
- MailKit
- ...
  

