# LenelConfigService

## Frontend (Angular)

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 20.3.6.

### Development server

First move to the HondaUI folder if not already.

```bash
cd HondaUI
```

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

Note: If Angular CLI is not installed globally, try the following to install:

```bash
npm list -g @angular/cli 
```

If this command indicates it is not installed, try:

```bash
sudo npm install -g @angular/cli.
```

### Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Backend (.NET)

This guide explains how to start the ASP.NET Core Web API backend for the Honda Facilities Security project.  
It assumes you have .NET 8 SDK installed on your machine.

### 1. Move to the Project Directory

Open another terminal.

First move to the HondaUI folder if not already.

```bash
cd HondaUI
```

Then

```bash
cd LenelConfigService
```

To connect to the correct database, go to `HondaUI/LenelConfigService/appsettings.json` and update:

```json
"ConnectionStrings": {
  "LenelExtractDB": "Server=ahmuowlrsq1001,1433;Database=LENELEXTRACT;User Id=AMU\\APP-LenelBadge-R-P;Password=wsMnrN4#C!;TrustServerCertificate=True;"
}
```

Note: Since we don't have access to the database to test, there may be some problems.  
Please debug as you can. For instance, in `Configuration.cs` under `LenelConfigService/Models`, the `[Column("column name")]` attribute should match the real column name in the database.

### 2. Restore Dependencies

Download all NuGet packages required by the project:

```bash
dotnet restore
```

### 3. (Optional) Build the Project

```bash
dotnet build
```

If the build succeeds, you’re ready to launch the API.

### 4. Run the Backend Server

Start the web API using:

```bash
dotnet run
```

The Endpoints dashboard will start with an empty list since there isn’t any data in the database yet. To add one, click the Add New button. You can then click any endpoint in the list to update an existing entry.

### 5. Access the API via Swagger

- <http://localhost:5062/swagger>

## Database Setup Guide (macOS with Docker); Ignore if you are running this in Honda environment

This section explains how to install SQL Server on macOS using Docker, create the `LenelExtract` database, and initialize the `Configurations` table schema.

---

### 1. Install Docker

If you haven’t installed Docker yet:

1. Go to [https://www.docker.com/products/docker-desktop/](https://www.docker.com/products/docker-desktop/)
2. Download **Docker Desktop for Mac (Apple Silicon or Intel)** depending on your chip.
3. Install and start Docker Desktop.
4. Confirm installation with:

```bash
docker --version
```

You should see something like:

```bash
Docker version 27.x.x, build xxxxxxx
```

---

### 2. Download and Run SQL Server in Docker

Run the following command to pull and start SQL Server 2022:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Zao127354@" \
   -p 1433:1433 --name sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

Explanation:

- `ACCEPT_EULA=Y` – accepts the SQL Server license
- `SA_PASSWORD` – sets the system administrator password (use the same password I set)
- `-p 1433:1433` – exposes SQL Server on port 1433
- `--name sqlserver` – names the container `sqlserver`
- `-d` – runs in the background (detached mode)

To confirm it’s running:

```bash
docker ps
```

You should see a container named `sqlserver` running.

---

## 3. Connect to SQL Server (macOS)

You can connect using the built-in SQL command-line tool or Azure Data Studio.

### Option 1 – Use `sqlcmd` in Docker

Run this command to open a SQL shell inside the container:

```bash
docker exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "Zao127354@" -No
```

If that path doesn’t work, install the SQL tools locally (see Option 2).

### Option 2 – Install Azure Data Studio (GUI)

1. Download from [https://learn.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio](https://learn.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio)
2. Open Azure Data Studio
3. Connect using:
   - Server: `localhost`
   - Authentication: `SQL Login`
   - Username: `sa`
   - Password: `Zao127354@`
   - Port: `1433`

---

## 4. Create the Database

Once connected to SQL Server, create a new database:

```sql
CREATE DATABASE LenelExtract;
GO
```

Switch to that database:

```sql
USE LenelExtract;
GO
```

---

## 5. Create the `EndpointConfigurations` Table

Run the following schema script:

```sql
DROP TABLE IF EXISTS Configurations;
GO

CREATE TABLE ExtractConfigurations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EndpointName NVARCHAR(100) NOT NULL UNIQUE,
    EmplId BIT,
    BadgeId BIT,
    LastName BIT,
    FirstName BIT,
    MiddleName BIT,
    ContractNumber BIT,
    NetId BIT,
    CompanyName BIT,
    CreateProgram BIT,
    CreateTimestamp BIT,
    RowId BIT,
    RowStamp BIT,
    LastChanged BIT,
    BadgeType BIT,
    Frequency INT CHECK (Frequency >= 0),
    Url NVARCHAR(255) NOT NULL UNIQUE,
    Status BIT NOT NULL DEFAULT 1
);
GO
```

---

## 6. Verify the Table

Check that the table exists:

```sql
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Configurations';
```

You should see one result with the name `Configurations`.

Optionally, insert a sample row:

```sql
INSERT INTO Configurations (Endpoint_name, EMPLID, BADGEID, LAST_NAME, FIRST_NAME, Frequency, URL)
VALUES ('Kronos', 1, 1, 1, 1, 30, 'https://downstream.example/api');
GO
```

Then verify with:

```sql
SELECT * FROM Configurations;
GO
```

---
