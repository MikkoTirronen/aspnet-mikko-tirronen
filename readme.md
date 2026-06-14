# CoreFitness

A full-stack gym portal built with ASP.NET Core MVC, Entity Framework Core, PostgreSQL, ASP.NET Identity, and Domain-Driven Design (DDD).

The application is based on a Figma design and allows users to register accounts, purchase memberships, browse gym classes, manage bookings, and maintain their account through a modern responsive interface.

> **Note:** This project was developed as part of an ASP.NET Core assignment with a focus on Clean Architecture, Domain-Driven Design (DDD), authentication, authorization, data persistence, and responsive UI implementation from a provided Figma design.

---

## Features

### Authentication & Authorization

* ASP.NET Core Identity
* User registration
* User login
* User logout
* Protected member-only pages
* Account management
* Role-ready architecture for future expansion

### Account Management

Users can manage their account through a dedicated profile area.

Features include:

* View profile information
* Membership overview
* View booked classes
* Account statistics
* Secure logout
* Permanent account deletion
* Automatic cleanup of associated memberships and bookings when an account is deleted

### Membership Management

Available membership plans:

* Basic
* Premium
* Elite

Features:

* Membership purchase flow
* Active membership tracking
* Membership status page
* Membership expiration support
* Different membership tiers for future business rules

### Gym Classes

Members can:

* Browse available classes
* View class details
* See instructor information
* View class capacity
* View class schedules
* Browse class categories

### Booking System

Members can:

* Book classes
* Cancel bookings
* View booked classes in their profile
* Prevent duplicate bookings
* Automatically update class availability

### Responsive UI

* Responsive navigation
* Mobile burger menu
* Training dropdown navigation
* Responsive membership cards
* Responsive profile page
* Responsive booking pages
* Figma-based design implementation

---

## Architecture

The solution follows Clean Architecture and Domain-Driven Design principles.

```text
src
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Domain Rules
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   ├── Interfaces
│   └── Handlers
│
├── Infrastructure
│   ├── EF Core
│   ├── PostgreSQL
│   ├── Identity
│   ├── Repositories
│   └── Services
│
└── Presentation.WebApp
    ├── Controllers
    ├── Razor Views
    ├── View Models
    ├── CSS
    └── Frontend Assets
```

---

## Domain-Driven Design Approach

The project separates responsibilities into distinct layers.

### Domain Layer

Contains:

* GymClass
* GymClassBooking
* Membership

Responsible for:

* Business rules
* Domain behavior
* Entity state management

### Application Layer

Contains:

* Commands
* Queries
* DTOs
* Handlers
* Abstractions

Examples:

* CreateMembershipCommand
* BookClassCommand
* CancelBookingCommand
* DeleteAccountCommand
* GetMembershipQuery
* GetClassDetailsQuery
* GetUserProfileQuery

### Infrastructure Layer

Contains:

* PostgreSQL persistence
* Entity Framework Core
* ASP.NET Identity
* Repository implementations
* Service implementations

### Presentation Layer

Contains:

* MVC Controllers
* Razor Views
* View Models
* Navigation
* Styling

Controllers remain thin by delegating business logic to the Application layer through command and query dispatchers.

---

## Technologies

### Backend

* ASP.NET Core MVC
* C#
* Entity Framework Core
* PostgreSQL
* ASP.NET Identity

### Frontend

* Razor Views
* HTML5
* CSS3
* Tailwind CSS build pipeline

### Architecture

* Domain-Driven Design (DDD)
* Clean Architecture
* CQRS-inspired Command/Query separation
* Repository Pattern
* Unit of Work Pattern

---

## Main Features Implemented

### Membership Feature

Users can:

* Purchase memberships
* View active memberships
* Track membership status

Implemented using:

* Commands
* Queries
* Repositories
* DTOs

### Gym Class Feature

Users can:

* Browse classes
* View class details
* See instructor information
* Track available capacity

### Booking Feature

Users can:

* Book classes
* Cancel bookings
* View booked classes from their profile

Business rules include:

* Duplicate booking prevention
* Capacity tracking

### Profile Feature

Users can:

* View profile information
* View membership details
* View bookings
* Logout
* Delete account

Deleting an account removes:

* Membership records
* Booking records
* Identity account

through an Application-layer command and handler.

---

## Navigation

Current site navigation includes:

### Main Navigation

* Fitness Centers
* Memberships
* Training
* Customer Service
* Store

### Training Dropdown

* Personal Training
* Group Training
* Classes

### Account Area

* About Me
* My Membership
* My Bookings
* Remove Account

Navigation is responsive and collapses into a burger menu on smaller devices.

---

## Database

The application uses PostgreSQL.

Primary tables:

```text
AspNetUsers
Memberships
GymClasses
Bookings
```

Entity Framework Core migrations are used for schema management.

---

## Running the Project

### Prerequisites

* .NET 10 SDK
* PostgreSQL
* Node.js

### Clone Repository

```bash
git clone https://github.com/yourusername/corefitness.git
cd corefitness
```

### Configure Database

Update:

```json
appsettings.json
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your PostgreSQL connection string"
  }
}
```

### Apply Migrations

```bash
dotnet ef database update \
--project src/Infrastructure \
--startup-project src/Presentation.WebApp
```

### Run Application

```bash
dotnet run --project src/Presentation.WebApp
```

---

## Learning Objectives

This project was built to gain practical experience with:

* ASP.NET Core MVC
* Entity Framework Core
* PostgreSQL
* ASP.NET Identity
* Authentication & Authorization
* Domain-Driven Design
* Clean Architecture
* CQRS patterns
* Repository Pattern
* Unit of Work Pattern
* Responsive UI implementation from Figma

---

## Future Improvements

Potential future enhancements:

* Profile image uploads
* Membership renewals
* Payment integration
* Email notifications
* Instructor management
* Admin dashboard
* Fitness progress tracking
* Class waitlists
* Membership analytics

---

## Author

**Mikko Tirronen**

Full Stack Developer student focused on:

* C#
* .NET
* ASP.NET Core
* PostgreSQL
* Full-stack web development
