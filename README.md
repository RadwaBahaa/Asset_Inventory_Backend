# Asset Inventory Management API

## Overview

This project is an ASP.NET-based RESTful API designed to manage and track asset inventory from suppliers to warehouses, and from warehouses to stores. The API facilitates efficient tracking of assets, ensuring streamlined operations and better management of the supply chain.

## Features

- **Asset Management**: CRUD operations for assets.
- **Supplier, Warehouse, and Store Management**: CRUD operations for suppliers, warehouses, and stores.
- **Tracking**: Track assets from suppliers to warehouses and from warehouses to stores.
- **Spatial Data Handling**: Utilizes NetTopologySuite.Geometries for managing spatial data points in a spatial database (SDE).
- **Authorization**: Implemented using `AuthorizationHandler` and `IAuthorizationRequirement`.
- **CORS Policy**: Configured to support integration with a React frontend application.

## Technologies Used

- **ASP.NET Core**: For building the RESTful API.
- **NetTopologySuite**: For handling geometric data types.
- **Entity Framework Core**: For database access and management.
- **Spatial Database (SDE)**: For managing spatial data.
- **AutoMapper**: For object mapping.
- **n-Tier Architecture**: Structured with Models, DTOs, Services, Context, Repository, and Presentation layers.

## Architecture

The API is structured using n-tier architecture, ensuring separation of concerns and facilitating maintainability and scalability:

- **Models**: Define the data structures.
- **DTOs (Data Transfer Objects)**: Used for transferring data between layers.
- **Services**: Contain business logic.
- **Context**: Manages database connections.
- **Repository**: Handles data access logic.
- **Presentation**: API controllers and endpoints.

## Entity Relationship Diagram

Below is the Entity Relationship Diagram (ERD) for the project, illustrating the relationships between different entities:

![Entity Relationship Diagram](./Media/Entity%20Relationship%20Diagram.jpg)

You can also view the diagram using the following link: [Entity Relationship Diagram](https://miro.com/welcomeonboard/NlhVSGpOTko5Nm4zRGpVWUx0TFdKQkVwZms5T3BCNWppQmM1MGl1UFdKdmlJYmlaSGNHU1JWVkJ1eVNHWHRkbHwzNDU4NzY0NTkyNDI1MjQ0MTQ3fDI=?share_link_id=628443707809)

## Installation

1. **Clone the repository**:

   ```sh
   git clone https://github.com/RadwaBahaa/Asset_Inventory_Backend
   cd AssetInventoryManagementAPI
   ```

2. **Setup the database**:

   - Ensure you have a spatial database (SDE) setup.
   - Update the connection string in `appsettings.json` to point to your database.

3. **Restore dependencies**:

   ```sh
   dotnet restore
   ```

4. **Apply migrations**:

   ```sh
   dotnet ef database update
   ```

5. **Run the application**:
   ```sh
   dotnet run
   ```

## API Endpoints

### Assets

- **GET /api/assets**: Retrieve all assets.
- **GET /api/assets/{id}**: Retrieve a specific asset by ID.
- **POST /api/assets**: Create a new asset.
- **PUT /api/assets/{id}**: Update an existing asset.
- **DELETE /api/assets/{id}**: Delete an asset.

### Suppliers

- **GET /api/suppliers**: Retrieve all suppliers.
- **GET /api/suppliers/{id}**: Retrieve a specific supplier by ID.
- **POST /api/suppliers**: Create a new supplier.
- **PUT /api/suppliers/{id}**: Update an existing supplier.
- **DELETE /api/suppliers/{id}**: Delete a supplier.

### Warehouses

- **GET /api/warehouses**: Retrieve all warehouses.
- **GET /api/warehouses/{id}**: Retrieve a specific warehouse by ID.
- **POST /api/warehouses**: Create a new warehouse.
- **PUT /api/warehouses/{id}**: Update an existing warehouse.
- **DELETE /api/warehouses/{id}**: Delete a warehouse.

### Stores

- **GET /api/stores**: Retrieve all stores.
- **GET /api/stores/{id}**: Retrieve a specific store by ID.
- **POST /api/stores**: Create a new store.
- **PUT /api/stores/{id}**: Update an existing store.
- **DELETE /api/stores/{id}**: Delete a store.

## Spatial Data

The API uses NetTopologySuite to manage spatial data for suppliers, warehouses, and stores. Each of these entities has a geographic location represented by points, enabling precise tracking and management.

## License

This project is licensed under the MIT License.

## Contact

For any questions or inquiries, please contact:

- **Name**: Radwa Bahaa El-Deen Abd El-Sabour
- **Email**: radwabahaaeldeen@gmail.com
- **LinkedIn**: https://www.linkedin.com/in/radwabahaaeldeen
