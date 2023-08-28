# Beer Tap Dispenser API

## Introduction

The Beer Tap Dispenser API is designed to manage self-service beer tap dispensers for festivals and events. It allows organizers to set up bar counters where attendees can serve themselves beer, calculate the amount dispensed, and track usage statistics.

## Features

- Create and manage dispensers with flow volume, cost, and status.
- Record dispenser usage with start time, end time, and calculated amount.
- Retrieve dispenser and usage information.
- Retrieve total liters and cost per dispenser.

## Getting Started

Follow these steps to set up and run the Beer Tap Dispenser API:

1. Clone this repository: `git clone https://github.com/juan-gsan/beer-taps.git`
2. Set up your development environment.
   - Install required dependencies: `dotnet restore`
   - Configure your database connection in `appsettings.json`.
   - Run database migrations: `dotnet ef database update`
3. Run the API: `dotnet run`
4. Access the API at: `http://localhost:5097/swagger` (or the specified port in your configuration)

## API Endpoints

### Beer

- `GET /Beer`: Retrieve a list of all dispensers.
- `GET /Beer/{id}`: Retrieve details of a specific dispenser.
- `POST /Beer`: Create a new dispenser.
- `DELETE /Beer/{id}`: Delete an existing dispenser.

### Usage

- `GET /Usage`: Retrieve a list of all dispenser usages.
- `GET /Usage/{id}`: Retrieve details of a specific dispenser usage.
- `POST /Usage`: When the user opens a tap. Create a new dispenser usage.
- `PATCH /Usage/{id}`: When the user closes a tap. Update an existing dispenser usage's details.

## Data Models

### Dispenser

- Id: int
- BeerName: string
- FlowVolume: decimal
- Cost: decimal
- Status: bool
- CreatedAt: DateTime
- UpdatedAt: DateTime
- TotalAmount: decimal
- TimesUsed: int

### DispenserUsage

- Id: int
- DispenserId: int (foreign key to Dispenser)
- StartTime: DateTime
- EndTime: DateTime
- Amount: decimal
- Cost: decimal

## Technologies

- .NET
- C#
- SQL (SQL Server)

## Contact

If you have any questions, suggestions, or feedback, please feel free to contact me at: juan.gsanchez13@gmail.com
