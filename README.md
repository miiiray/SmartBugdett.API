\# SmartBudget API



A layered ASP.NET Core Web API for personal budget management.



\## Overview



SmartBudget API is designed to help users manage their personal finances by tracking incomes, expenses, and categories. The project follows Clean Layered Architecture principles and focuses on maintainability, scalability, and secure authentication.



\## Features



\- JWT Authentication \& Authorization

\- User Management

\- Expense Management

\- Income Management

\- Category Management

\- AI-powered Budget Analysis

\- FluentValidation

\- AutoMapper

\- Generic Repository

\- Unit of Work

\- Entity Framework Core

\- SQL Server Integration

\- Swagger API Documentation



\## Architecture



```

SmartBudget.API

│

├── Controllers

├── Middleware

│

SmartBudget.Business

│

├── Managers

├── Services

├── ValidationRules

│

SmartBudget.DataAccess

│

├── Context

├── Repositories

├── Configurations

├── UnitOfWork

│

SmartBudget.DTO

│

├── Auth

├── Users

├── Expenses

├── Categories

├── Incomes

│

SmartBudget.Entities

│

├── User

├── Expense

├── Income

├── Category

│

SmartBudget.Core

```



\## Technologies



\- ASP.NET Core

\- C#

\- Entity Framework Core

\- SQL Server

\- JWT Authentication

\- AutoMapper

\- FluentValidation

\- OpenAI API

\- Swagger



\## Security



\- JWT Bearer Authentication

\- User-based authorization

\- Ownership validation

\- Password hashing

\- Secure API endpoints



\## AI Features



The application includes an AI-powered budget analysis service that analyzes user income and expense data and generates personalized financial recommendations using the OpenAI API.



\## Future Improvements



\- Monthly budget reports

\- Savings goals

\- Dashboard \& Analytics

\- Notifications

\- Export to PDF \& Excel

\- Spending forecasts



\## Getting Started



```bash

git clone <repository-url>



cd SmartBudget.API

```



Install dependencies and configure your SQL Server connection string before running the project.



\## Author



Developed as a layered ASP.NET Core backend project for learning modern backend architecture and AI integration.

