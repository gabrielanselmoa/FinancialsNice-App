# 📊 FinNice: Master Your Finances with Ease

**FinNice** is your ultimate financial management solution, built with **ASP.NET Core 8** and **Entity Framework Core**. It's designed for effortless control over your money, leveraging a **Code-First** approach and integrating modern technologies like **MailKit** for email communication and **MinIO** for scalable object storage.

---

## ✨ Why Choose FinNice?

- **Effortless Financial Control**: Take charge of your income, expenses, and budgets with intuitive tools.
- **Stay Informed**: Receive timely notifications and reports via email.
- **Secure & Scalable Storage**: Safely store documents and media with **MinIO** (S3-compatible).
- **Modern & Robust**: Built on a clean architecture with **.NET 8** and **EF Core** for performance and reliability.

> Transform your financial habits and gain peace of mind with **FinNice**!

---

## 🏛️ Architecture & Best Practices

**FinNice** employs a **modular monolith architecture**, balancing the agility of a monolith with modular benefits. This ensures:

- Clear separation of concerns
- Simplified maintenance
- Scalable growth

### Clean Architecture Principles

- **Framework Independence**: Core logic decoupled from external frameworks
- **Testability**: Business rules are easily testable
- **Scalability & Maintainability**: Designed for the long term

### Key Design Patterns & Best Practices

- **Clean Architecture**
- **Modular Monolith**
- **SOLID Principles**
- **Repository Pattern**
- **Service Pattern**
- **Dependency Injection (DI)**
- **Comprehensive Data Validation**
- **Global Exception Handling**
- **DTOs (Data Transfer Objects)**

---

## 🚀 Technologies

| Technology        | Description                          |
|-------------------|--------------------------------------|
| C#                | Primary programming language         |
| .NET 8            | Development platform                 |
| EF Core           | Code-First ORM                       |
| PostgreSQL 17     | Relational database                  |
| MailKit           | Email integration                    |
| MinIO             | S3-compatible object storage         |
| Docker            | Containerization and deployment      |

---

## 🧰 Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/downloads)

---

## 🔧 Setup & Execution

### 1. Clone the Repository

```bash
git clone https://github.com/gabrielanselmoa/FinNiceApp.git
cd FinNice
```

### 2. Configure Your Environment

> Create a .env file in the project root with your database and service configurations.

### 3. Build & Run the Application
```bash
docker build -t finnice-app .
docker run --rm -p 8080:80 --env-file .env finnice-app
```

### 4. Apply the migrations
```bash
dotnet ef database update
```

---

## 📜 License

**MIT License with Attribution Requirement**  
Copyright (c) 2025 Gabriel Anselmo

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”), to deal in the Software without restriction, including without limitation the rights to:

- use,
- copy,
- modify,
- merge,
- publish,
- distribute,
- sublicense,
- and/or sell copies of the Software,

and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

1. The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

2. **Attribution Required**: If you use, copy, modify, or distribute this Software as part of a project that is **substantially based on or derived from it**, you must:

    - Give appropriate credit to **Gabriel Anselmo**
    - Provide a link to the original repository
    - Indicate if changes were made

> This helps others understand the origins of the project and gives proper credit to the original author.

---

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
