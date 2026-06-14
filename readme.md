# CoreFitness Gym Portal

A full-stack ASP.NET Core MVC gym portal built using Domain-Driven Design (DDD) and Clean Architecture principles.

The application allows users to register accounts, purchase memberships, browse fitness classes, book and cancel classes, manage their profile, and authenticate using either local accounts or GitHub OAuth.

---

# Features

## Authentication & Authorization

* ASP.NET Core Identity
* Email/password registration and login
* GitHub OAuth login
* Logout functionality
* Delete account functionality
* Role-based authorization
* Seeded administrator account

## Memberships

* Standard Membership
* Premium Membership
* Membership status page
* Active membership tracking
* Membership management from user profile

## Fitness Classes

* Browse available classes
* View class details
* Book classes
* Cancel bookings
* Capacity tracking
* Booking restrictions based on membership

## User Profile

* Update profile information
* View membership information
* View booked classes
* Client-side validation
* Server-side validation

## Admin Features

Administrators can:

* Create gym classes
* Edit gym classes
* Delete gym classes
* Manage available training sessions

## Additional Pages

* Home page
* Membership page
* Customer Service page
* Custom 404 page

---

# Getting Started

## Prerequisites

* .NET 10 SDK
* Node.js & npm
* Git

## Clone Repository

```bash
git clone <repository-url>
cd aspnet-mikko-tirronen
```

## Restore Packages

```bash
dotnet restore
```

## Install Frontend Dependencies

```bash
cd src/Presentation.WebApp
npm install
```

## GitHub OAuth Setup (Optional)

Initialize User Secrets:

```bash
dotnet user-secrets init
```

Add GitHub OAuth credentials:

```bash
dotnet user-secrets set "Authentication:GitHub:ClientId" "<client-id>"
dotnet user-secrets set "Authentication:GitHub:ClientSecret" "<client-secret>"
```

## Run Application

```bash
dotnet run --project src/Presentation.WebApp
```

Tailwind CSS is automatically compiled during the build process.

Development uses an Entity Framework InMemory database automatically.

Application URLs:

```text
http://localhost:3000
https://localhost:7225
```

---

# Seeded Administrator Account

The application automatically seeds an administrator account during startup.

```text
Email: admin@test.com
Password: Administrator@1
```

Roles:

```text
Admin
Member
```

All newly registered users are automatically assigned the Member role.


---

## GitHub OAuth Notes

GitHub OAuth requires the application to run over HTTPS.

The OAuth application should be configured with:

```text
Homepage URL:
https://localhost:7225

Authorization callback URL:
https://localhost:7225/signin-github
```

If the application is started using the HTTP profile, GitHub authentication will not function correctly.

Run the HTTPS launch profile when testing GitHub login.


---

# Architecture

The solution follows Clean Architecture and DDD principles.

## Domain

Contains:

* Entities
* Business Rules
* Domain Exceptions

## Application

Contains:

* Commands
* Queries
* Handlers
* DTOs
* Repository Abstractions
* Result Pattern

## Infrastructure

Contains:

* Entity Framework Core
* ASP.NET Identity
* Repository Implementations
* Database Configuration
* GitHub OAuth Configuration 

## Presentation.WebApp

Contains:

* MVC Controllers
* Razor Views
* View Models
* Client-side Assets
* UI Styling

---

# Technologies

* ASP.NET Core MVC
* .NET 10
* Entity Framework Core
* ASP.NET Identity
* PostgreSQL
* Entity Framework InMemory Database
* GitHub OAuth
* Tailwind CSS v4
* Font Awesome
* xUnit
* FluentAssertions

---

# Database Configuration

## Development

Development environments use:

```text
Entity Framework InMemory Database
```

## Production

Production environments use:

```text
PostgreSQL
```

The database provider is selected automatically based on the current environment.

---

# Validation

## Server-side Validation

Implemented using:

* Data Annotations
* ModelState Validation

## Client-side Validation

Implemented using:

* JavaScript Validation
* Password Strength Validation
* Profile Form Validation

---

# Testing

The solution includes both Unit Tests and Integration Tests.

## Integration Tests

Covered scenarios include:

* Membership creation
* Class booking
* Duplicate booking prevention
* Booking cancellation
* Gym class creation
* Gym class updates
* Gym class deletion

Run all tests:

```bash
dotnet test
```

---

# Design Patterns & Practices

The project implements:

* Domain-Driven Design (DDD)
* Clean Architecture
* CQRS (Command Query Responsibility Segregation)
* Repository Pattern
* Result Pattern
* Domain Exception Pattern
* Dependency Injection
* Role-Based Authorization

---

# Project Goals

The project was developed as part of an ASP.NET Core assignment with focus on:

* Domain-Driven Design
* Clean Architecture
* Authentication & Authorization
* Data Persistence
* Testing
* Maintainability
* Separation of Concerns
* Full-Stack Web Development
