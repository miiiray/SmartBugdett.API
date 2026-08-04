<div align="center">

# 💰 SmartBudget API

### Personal Budget Management System built with ASP.NET Core

![.NET](https://img.shields.io/badge/.NET-9-purple?style=for-the-badge)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-blue?style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-Backend-239120?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?style=for-the-badge)
![OpenAI](https://img.shields.io/badge/OpenAI-AI-green?style=for-the-badge)

</div>

---

# 📖 About

SmartBudget API is a layered ASP.NET Core Web API designed for managing personal finances.

The project enables users to securely manage:

- 💵 Incomes
- 💸 Expenses
- 📂 Categories
- 🤖 AI Budget Analysis

---

# ✨ Features

✅ JWT Authentication

✅ User Authorization

✅ Expense Management

✅ Income Management

✅ Category Management

✅ AI Budget Analysis

✅ FluentValidation

✅ AutoMapper

✅ Generic Repository

✅ Unit of Work

✅ Swagger Documentation

---

# 🏗️ Project Architecture

```text
📦 SmartBudget.API
 ┣ 📂 Controllers
 ┣ 📂 Mapping

📦 SmartBudget.Business
 ┣ 📂 Managers
 ┣ 📂 ValidationRules

📦 SmartBudget.DataAccess
 ┣ 📂 Context
 ┣ 📂 Configurations
 ┣ 📂 Repositories
 ┣ 📂 UnitOfWork

📦 SmartBudget.DTO

📦 SmartBudget.Entities

📦 SmartBudget.Core
```

---

# ⚙️ Tech Stack

| Technology | Purpose |
|------------|----------|
| ASP.NET Core | Web API |
| C# | Backend |
| Entity Framework Core | ORM |
| SQL Server | Database |
| JWT | Authentication |
| AutoMapper | Object Mapping |
| FluentValidation | Validation |
| OpenAI API | AI Budget Analysis |
| Swagger | API Documentation |

---

# 🚀 Local Setup

1. Configure local secrets from the repository root. Never commit these values:

```powershell
$jwtKey = [Convert]::ToBase64String([Security.Cryptography.RandomNumberGenerator]::GetBytes(64))
dotnet user-secrets set "Jwt:SecurityKey" "$jwtKey" --project SmartBugdett.API/SmartBudgett.API.csproj
dotnet user-secrets set "AiSettings:OpenAIApiKey" "YOUR_OPENAI_API_KEY" --project SmartBugdett.API/SmartBudgett.API.csproj
```

2. Apply the database migrations:

```powershell
Push-Location SmartBugdett.API
dotnet tool restore
dotnet ef database update --project ../SmartBudgett.DataAccess --startup-project .
Pop-Location
```

The data-integrity migration does not delete invalid legacy data. If it stops,
read the SQL error, correct the duplicate or orphaned record, and run the update again.

3. Start the API:

```powershell
dotnet run --project SmartBugdett.API
```

Swagger is available in the Development environment at `/swagger`.

## Protected user endpoints

The user endpoints always operate on the identity in the JWT. A client cannot select another user ID.

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/User/me` | Get the signed-in user |
| PUT | `/api/User/me` | Update the signed-in user |
| DELETE | `/api/User/me` | Delete the signed-in user |
| POST | `/api/Ai/budget-analysis` | Generate the signed-in user's budget analysis |

---

# 🔐 Security

- 🔑 JWT Bearer Authentication
- 👤 User-based Authorization
- 🔒 Password Hashing
- 🛡️ Ownership Validation
- 🔐 Secrets stored outside source control
- 🚦 Rate limiting for AI analysis

---

# 🤖 AI Integration

The project includes an AI-powered budget analysis service.

It analyzes users' income and expense data and generates personalized financial recommendations using the OpenAI API.

---

# 🚀 Future Improvements

- 📊 Dashboard
- 📈 Budget Reports
- 🎯 Savings Goals
- 🔔 Notifications
- 📄 PDF Export
- 📑 Excel Export

---

# 📌 Status

🟢 Active Development

---

<div align="center">

### ⭐ If you like this project, don't forget to leave a star.

</div>
