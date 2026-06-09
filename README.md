# The Cauldron

A full-stack game review web application built with Angular and .NET Core, hosted on Azure.

🌐 **Live site:** [thecauldron.xyz](https://www.thecauldron.xyz)

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | Angular (TypeScript) |
| Backend | ASP.NET Core (C#) |
| Database | SQL Server (Azure SQL) |
| Hosting | Azure App Service |
| Storage | Azure Blob Storage |
| Email | AWS SES |
| Auth | JWT (JSON Web Tokens) |

---

## Features

- **User authentication** — Custom JWT-based login and registration system
- **Game reviews** — Users can write and manage reviews for games
- **Rich text reviews** - Review editor powered by Quill with support for images and inline formatting
- **Transactional emails** — Account and notification emails powered by AWS SES
- **Cloud-hosted database** — SQL Server database managed on Azure SQL
- **Custom domain** — Deployed and served under a custom domain via Azure App Service

---

## Running Locally

### Prerequisites
- [Node.js](https://nodejs.org/) + Angular CLI
- Node.js also coveres the npm packages required as installed automatically via npm install
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- SQL Server or Azure SQL connection string

### Steps

1. **Clone the repo**
   ```bash
   git clone https://github.com/jnguyen5151/<repo-name>.git
   cd <repo-name>
   ```

2. **Configure the backend**

   Update `appsettings.json` with your connection string, JWT secret, and AWS SES credentials:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your-sql-connection-string"
     },
     "Jwt": {
       "Key": "your-secret-key"
     },
     "Aws": {
       "SesRegion": "us-east-1",
       "FromEmail": "your-email@example.com"
     }
   }
   ```

3. **Run the backend**
   ```bash
   cd api
   dotnet run
   ```

4. **Run the frontend**
   ```bash
   cd client
   npm install
   ng serve
   ```

5. Open `http://localhost:4200`

---

## Deployment

The application is deployed on **Azure App Service** with the database hosted on **Azure SQL**. The frontend is built and served as part of the same deployment pipeline using **GitHub Actions**.
