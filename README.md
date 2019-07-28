# Telecom Operator

## Project structure

- `TelecomOperatorApi`: Backend API, written using ASP.NET Core, Entity Framework Core
- `telecom-operator-client`: Frontend web, written using ReactJS

## Start project in local

- This project assumes Postgres DB is installed locally and available at port 5432 with username/password as specified in `TelecomOperatorApi/appsettings.Development.json`
- Start backend API either through:
    + Visual Studio
    + or command line
    ```
    dotnet run --project TelecomOperatorApi
    ```
- Start frontend web
    ```
    cd telecom-operator-client
    npm install
    npm start
    ```

- Run Tests
    ```
    dotnet test
    ```
    Note : integration tests required database, this assumed that postgres db is already installed locally and available at port 5432 with username/password as specified in `TelecomOperatorApi/appsettings.Test.json`

## Backend APIs

Backend offers 5 APIs:

- `GET /api/phones`

    get all phone numbers

- `GET /api/customers`

    get all customers

- `GET /api/customers/{customerId}/phones`

    get all phone numbers for customer with id {customerId}

- `POST /api/customers/{customerId}/phones`

    add new phone number for customer with id {customerId}

- `PUT /api/customers/{customerId}/phones/{phoneNumber}/activation`

    activate phone number {phoneNumber} for customer with id {customerId}

## Assumptions

- Phone number is 10 digits long, no country code, begins with 03 or 04
- Customer is pre-created and managed separately
- Upon creation of phone number, activation status will be false

## Demo

The demo applications are deployed to Azure App Service:

- Database (Postgres) is hosted at `https://www.elephantsql.com/`
- Backend API is hosted at Azure App Service with url: `https://telecomoperatorapi20190711114653.azurewebsites.net/`
- Frontend web is hosted at Azure App Service with url: `https://telecom-operator.azurewebsites.net/`
