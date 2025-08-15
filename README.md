# Bank Management System (Blazor Server, .NET 6)

A simple **bank management** web app built with **Blazor Server** and **Entity Framework Core** (SQL Server). It demonstrates account registration, balance lookups, and basic transactions (deposit, withdraw, transfer) with an admin view for quick management.

> This README was generated from the uploaded project to make it GitHub-ready. ✨

## ✨ Features

- **Account registration** with validation (Debit/Credit/Saving)
- **Account listing & details**
- **Transactions**: deposit, withdraw, transfer (with basic checks)
- **Admin panel** (password-gated) to access management actions
- **Toast notifications** for user feedback (via Blazored.Toast)
- **EF Core** persistence with SQL Server & migrations
- Clean **Blazor** component structure (Pages, Shared, Data layers)

## 🧱 Tech Stack

- **.NET**: NET6.0
- **UI**: Blazor Server (Razor Components)
- **Data**: Entity Framework Core (SqlServer provider)
- **Styling**: Bootstrap, Open Iconic (via `wwwroot/css`)
- **Packages**:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Blazored.Toast`

## 🚀 Getting Started

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- A SQL Server instance (LocalDB works fine), or update the connection string accordingly.

### 1) Clone & restore
```bash
git clone <your-repo-url>.git
cd BankManagementSystem
dotnet restore
```

### 2) Configure the database
Update **`appsettings.json`** with your SQL Server connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Banking;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

> You can also use **User Secrets** in development to avoid committing secrets. The project includes a `UserSecretsId` in the `.csproj` so you can run:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

### 3) Create the database
Apply Entity Framework migrations:
```bash
dotnet tool install --global dotnet-ef   # if not already installed
dotnet ef database update
```

### 4) Run the app
```bash
dotnet run
# then open the shown localhost URL (usually https://localhost:5001 or http://localhost:5000)
```

## 📂 Project Structure
```
BankManagementSystem/
├─ BankManagementSystem.csproj
├─ App.razor
├─ _Imports.razor
├─ Data/
│  ├─ AppDbContext.cs
│  ├─ Account.cs
│  ├─ Register.cs
│  ├─ BankingFunctions.cs
│  ├─ AccountService.cs
│  ├─ RegisterService.cs
│  └─ AdminService.cs
├─ Pages/
│  ├─ Index.razor
│  ├─ Register.razor
│  ├─ AdminPage.razor
│  ├─ AdminUserTable.razor
│  ├─ _Host.cshtml
│  ├─ _Layout.cshtml
│  └─ Error.cshtml(+.cs)
├─ Shared/
│  ├─ MainLayout.razor(+.css)
│  ├─ NavMenu.razor(+.css)
│  └─ SurveyPrompt.razor
├─ wwwroot/
│  └─ css/ (bootstrap, open-iconic, site.css)
├─ Migrations/ (EF Core migrations)
└─ appsettings.json
```

## 🔐 Admin Access
The sample admin check is implemented in `Data/AdminService.cs`. **Default password is `admin` (hard‑coded) for demo only.**

**Important:** Move admin auth to a proper identity system (e.g., ASP.NET Core Identity), store secrets outside source control, and never ship hard-coded credentials.

## 🧪 How It Works (High Level)

- **AppDbContext** defines the EF Core model (`RegisterModel`, `Account`) and database access.
- **Register flow** writes to `RegisterModels`, and a service method can **promote** registrations to `Accounts`.
- **Transactions** (deposit, withdraw, transfer) adjust balances with simple guards.
- **UI** lives under `Pages/` (`Index.razor`, `Register.razor`, `AdminPage.razor`, etc.), with layout in `Shared/`.

## 🛠 Commands Reference

- Run the dev server:
  ```bash
  dotnet run
  ```
- Add a new migration:
  ```bash
  dotnet ef migrations add <MigrationName>
  ```
- Apply migrations:
  ```bash
  dotnet ef database update
  ```

---
