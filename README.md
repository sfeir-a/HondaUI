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

To connect to the correct database, go to `HondaUI/LenelConfigService/appsettings.json` and update as needed.

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

## Database (On cloud)

### 1. SQL Server Credentials

- Server: `lenelextract-db.c9koiciwmqnp.us-east-2.rds.amazonaws.com`
- Authentication: `SQL Login`
- Username: `admin`
- Password: `admin123`
- Port: `1433`

---

### 2. `EndpointConfigurations` Schema

```sql
USE LENELEXTRACT;
GO

CREATE TABLE ExtractConfigurations (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    EndpointName NVARCHAR(100) NOT NULL UNIQUE,

    -- Feature toggles
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

    -- Execution frequency
    Frequency INT CHECK (Frequency >= 0),

    -- Endpoint URL
    Url NVARCHAR(255) NOT NULL UNIQUE,

    -- Credentials
    CredentialUser NVARCHAR(100) NULL,
    CredentialPassword VARBINARY(MAX) NULL,

    -- Status (1 = active, 0 = inactive)
    Status BIT NOT NULL DEFAULT 1
);
GO
```
