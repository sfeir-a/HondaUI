# MyAngularApp

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 20.3.6.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

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
