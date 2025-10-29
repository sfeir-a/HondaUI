# MyAngularApp

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 20.3.6.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

Note: If Angular CLI is not installed globally, try the following to install:

npm list -g @angular/cli 

If this command indicates it is not installed, try:

sudo npm install -g @angular/cli.



## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.

## LenelConfigService – Backend Setup Guide

This guide explains how to start the ASP.NET Core Web API backend for the Honda Facilities Security project.  
It assumes you have .NET 8 SDK installed on your machine.

## 1. Move to the Project Directory

```bash
cd LenelConfigService
```

## 2. Restore Dependencies

Download all NuGet packages required by the project:

```bash
dotnet restore
```

## 3. (Optional) Build the Project

```bash
dotnet build
```

If the build succeeds, you’re ready to launch the API.

## 4. Run the Backend Server

Start the web API using:

```bash
dotnet run
```

## 5. Access the API via Swagger

- <http://localhost:5062/swagger>

## Database Setup Guide (macOS with Docker)

This section explains how to install SQL Server on macOS using Docker, create the `LenelExtract` database, and initialize the `Configurations` table schema.

---

## 1. Install Docker

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

## 2. Download and Run SQL Server in Docker

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

## 5. Create the `Configurations` Table

Run the following schema script:

```sql
CREATE TABLE Configurations (
    Endpoint_name NVARCHAR(100) PRIMARY KEY,
    EMPLID BIT,
    BADGEID BIT,
    LAST_NAME BIT,
    FIRST_NAME BIT,
    MIDDLE_NAME BIT,
    CONTR_NO BIT,
    NET_ID BIT,
    COMPANY_NAME BIT,
    CREATE_PROGRAM BIT,
    CREATE_TSTP BIT,
    ROWID BIT,
    ROWSTAMP BIT,
    lastchanged BIT,
    BadgeType BIT,
    Frequency INT,
    URL NVARCHAR(255)
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
