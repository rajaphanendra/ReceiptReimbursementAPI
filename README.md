# Receipt Reimbursement API - Backend

## Overview
This repository contains the backend implementation of the Receipt Reimbursement System. It is built using ASP.NET Core Web API (.NET 8) and interacts with a Microsoft SQL Server database. The backend is responsible for handling all business logic, data persistence, and file storage for receipt submissions.

---

## Tech Stack
- **Framework:** ASP.NET Core Web API (.NET 8)
- **Database:** Microsoft SQL Server 2022
- **ORM:** Entity Framework Core
- **Containerization:** Docker

---

## Features
- Submit a receipt with details (amount, date, description, file upload).
- Retrieve all submitted receipts.
- Serve uploaded files for view/download.

---

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/products/docker-desktop) (for containerized usage)
- [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local or container)

---

## Environment Configuration

This project requires a SQL Server connection string. You can configure this using an environment variable or `appsettings.json`.

### Example Environment Variable

```
ConnectionStrings__DefaultConnection=Server=localhost,1433;Database=ReceiptDb;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;
```

If using Docker Compose, this variable is set in the `docker-compose.yaml` file via `.env`.

---

## Running the Application

### Option 1: Using Docker

Ensure Docker is installed and running.

```bash
docker build -t receipt-backend .
docker run -p 5267:8080 --env-file ../.env receipt-backend
```

### Option 2: Using .NET CLI

```bash
dotnet restore

# Run migrations if required
dotnet ef database update

# Run the application
dotnet run --project ReceiptReimbursementAPI.API
```

The API will be available at `http://localhost:5267` by default.

---

## API Endpoints

### Submit a Receipt
**POST** `/api/Receipt`

**Form-data Parameters:**
- `amount` (decimal)
- `date` (date)
- `description` (string)
- `file` (file upload: PDF/Image)

### Get All Receipts
**GET** `/api/Receipt`

Returns a list of all receipts with their metadata.

### View a File
Each receipt entry includes a file path URL that can be accessed via the frontend to download or view the file.

---

## Project Structure

```
ReceiptReimbursementAPI/
├── API/                 # Main API project
├── Application/         # Business logic
├── Data/                # Entity Framework DbContext and migrations
├── Model/               # Domain models
└── ReceiptReimbursementAPI.sln
```

---

## Notes
- File uploads are stored on the container filesystem by default. Ensure persistent volume mapping is configured in production.
- Update the `ConnectionStrings` in the environment file or deployment config if running outside Docker.