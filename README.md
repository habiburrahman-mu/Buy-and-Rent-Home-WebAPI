# Buy-and-Rent-Home-WebAPI

## Overview

The **Buy-and-Rent-Home-WebAPI** is a .NET 8 backend application designed to facilitate property buying and renting services. It is structured using **clean architecture** principles to promote maintainability, scalability, and separation of concerns.

## Architecture

This project follows the principles of clean architecture, which promotes a clear separation of concerns among different layers. The main layers include:

- **Presentation Layer**: Responsible for handling incoming requests and returning responses, typically through API controllers.
- **Application Layer**: Contains business logic and application services. This layer orchestrates the use of the domain and data access layers.
- **Domain Layer**: Holds core business entities, rules, and domain logic. This layer is independent of any frameworks or external services.
- **Data Access Layer**: Manages database interactions and provides a repository pattern to abstract data access.

This architecture allows for easy testing, maintenance, and adaptability to change.

## Project Structure

```
+---src
|   +---API                   // Entry point for the application, handling HTTP requests
|   |   |   Program.cs           // Main application entry
|   |   |   appsettings.json     // Application configuration settings
|   |   +---Controllers          // API controllers for handling incoming requests
|   |   +---Middlewares          // Custom middleware for request processing
|   +---Core                     // Core application logic and business rules
|   |   +---Application          // Application layer, including services and DTOs
|   |   |   +---DTOs             // Data Transfer Objects for communication
|   |   |   +---Interfaces       // Interfaces for services used in the application
|   |   |   +---Services         // Implementations of application services
|   |   +---Domain               // Domain layer containing business entities and rules
|   |       +---Entities         // Domain entities representing business objects
|   |       +---Interfaces       // Repository interfaces for data access
|   |       +---Exceptions       // Custom exceptions for domain logic
|   +---External                 // External integrations and infrastructure services
|   |   +---Infrastructure       // Implementation details for data access and services
|   |   |   +---Persistence     // Data access layer, including context and repositories
|   |   |   +---Services        // Infrastructure services (e.g., notification services)
|   |   |   +---Hubs             // SignalR hubs for real-time communication
|   |   +---Presentation         // Receive Request from API and send to Application
|   |       +---Controllers            
|   |       +---ViewModels       // View models to pass data
|   +---Shared                   // Shared resources, constants, and utilities
|   |   +---Common               // Common functionalities and constants
|   \---Upload                   // Folder for uploaded files
|       \---files                // Specific files related to uploads
```

### Key Components

- **API Layer**:
  - Contains `Program.cs`, configuration files, controllers, and middleware for handling requests.
- **Core Layer**:
  - **Application**: Includes DTOs, service interfaces, and implementations.
  - **Domain**: Consists of domain entities, repository interfaces, and domain exceptions.
- **External Layer**:
  - **Infrastructure**:
    - **Persistence**: Contains the database context and repository implementations for data access.
      - **Services**: Other infrastructure services like notification services or external integrations.
      - **Hubs**: For real-time communication (e.g., SignalR hubs).
    - **Presentation**
      - Controller: Controller accepts request from api layer and then pass it to Application layer.
- **Shared Layer**:
  - Contains shared utilities or constants used across the application.

### Technologies - Libraries

✔️ **.NET 8** - The latest version of the .NET framework, which includes ASP.NET and ASP.NET Core for building modern web applications.

✔️ **Microsoft.AspNetCore.Authentication.JwtBearer** - Library for handling JWT authentication in ASP.NET Core applications.

✔️ **Microsoft.EntityFrameworkCore** - A lightweight, extensible version of Entity Framework for .NET Core that provides a set of tools for data access.

✔️ **Microsoft.AspNetCore.SignalR** - A library for adding real-time web functionality to applications, allowing for bi-directional communication between client and server.

✔️ **Microsoft.AspNetCore.SignalR.Common** - Contains common functionalities and types used across SignalR.

✔️ **AutoMapper** - A convention-based object-object mapper that simplifies the process of mapping between different data models in .NET applications.

## Configuration

Configuration settings are managed in the `appsettings.json` and `appsettings.Development.json` files located in the `API` folder. These files allow you to configure database connections, logging, and other settings.

## Middleware

The application uses middleware for handling exceptions and WebSocket connections. The key middleware components include:

- **ExceptionMiddleware**: Catches unhandled exceptions and returns a standardized error response.

## Services

The application consists of multiple services that encapsulate business logic. Each service is responsible for a specific area of functionality, ensuring the separation of concerns and promoting reusability. Services interact with the application layer and the data access layer to fulfill business requirements.

## Data Access

Data access is managed through repositories that abstract the underlying data storage mechanisms. This pattern allows for easier unit testing and provides flexibility in changing the data source without affecting the rest of the application.

## API Flow

The data flow in the **Buy-and-Rent-Home-WebAPI** is designed to facilitate seamless communication between clients and the server while adhering to the principles of clean architecture. Below is a breakdown of how data flows through the different layers of the application:

![API Flow Diagram](docs-asset/api%20flow-diagram.png)

### 1. Client Request

- **Initiation**: The data flow begins with a client (e.g., web or mobile application) sending an HTTP request to the API. This request can be for various operations, such as retrieving property listings, creating a visiting request, or user authentication.
- **Request Structure**: The request typically includes the necessary data in the body (for POST, PUT requests) or parameters (for GET requests), along with headers for authentication and content type.

### 2. Presentation Layer (API Controllers)

- **Handling Requests**: The API controllers in the `src/API/Controllers` directory are responsible for receiving incoming requests. Each controller corresponds to a specific domain entity (e.g., `PropertyController`, `UserController`).
- **Validation**: Upon receiving a request, the controller validates the input data, checking for required fields, data formats, and any business logic constraints. If validation fails, an appropriate error response is returned.

### 3. Application Layer (Services)

- **Business Logic Execution**: If the input is valid, the controller calls the relevant service in the application layer (located in `src/Core/Services`). The service contains the business logic required to process the request.
- **Data Manipulation**: The service may perform various operations, such as creating, updating, or retrieving data. It can also involve complex logic, like checking user permissions or applying business rules.

### 4. Data Access Layer (Repositories)

- **Database Interaction**: After processing the request, the service interacts with the data access layer, which consists of repositories. These repositories abstract the underlying data access and handle interactions with the database using Entity Framework Core.
- **CRUD Operations**: The repositories perform Create, Read, Update, and Delete (CRUD) operations based on the service's requirements. The results are typically returned as domain entities or DTOs.

### 5. Response Generation

- **Transforming Data**: Once the data is retrieved or manipulated, the service may convert the domain entities to Data Transfer Objects (DTOs) using mapping profiles (found in `src/Core/MapperProfiles`) for a more structured response.
- **Response Sending**: The controller prepares the response, often including status codes (e.g., 200 OK, 201 Created, 400 Bad Request) and the resulting data (if applicable). This response is then sent back to the client.

### 6. Client Response

- **Data Handling**: The client receives the response from the API and processes it accordingly. This may involve displaying data to the user, handling errors, or triggering additional actions based on the response.

### 7. Real-Time Communication (Work In Progress)

- **SignalR Integration**: If the request involves real-time updates (e.g., chat messages), the service may utilize SignalR to push notifications to connected clients, allowing for instantaneous updates without needing to refresh the page.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (for database setup)
- [Visual Studio](https://visualstudio.microsoft.com/) or any preferred IDE

### Installation

1. Clone the repository:

    ```bash
    git clone <this-repository-url>
    cd Buy-and-Rent-Home-WebAPI
    ```

2. Restore dependencies:

    ```bash
    dotnet restore
    ```

3. Run the application:

    ```bash
    dotnet run --project src/API/API.csproj
    ```

## Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss potential improvements.

## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE Version 2, June 1991. See the [LICENSE.txt](LICENSE.txt) file for more details.
