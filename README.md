# Bike 360 🚴‍♂️🚴‍♀️  
**Bike 360** is a bike rental web application built with .NET to provide a seamless experience for users to browse, rent, and manage bike rentals.

## Table of Contents
- [Project Phases](#project-phases-📅)
- [Technologies](#technologies-🛠️)
- [Installation](#installation-⚙️)
- [Features](#features-✨)



## Project Phases 📅

### PHASE 1: 🚀 Initial Core Features
- **Basic Reservation Functionality**: Users can browse and reserve available bikes.
- **Functional UI**: A responsive and intuitive UI, enabling seamless interactions (ideally build with React)
- **Identity & Roles**: Registered users can make reservations, while administrators have enhanced permissions.

### PHASE 2: 📢 Expanding User Interactions
- **Notifications**: Users receive real-time in-app and email notifications for booking confirmations, updates, and reminders.
- **Bike Reviews**: A feature that allows users to leave reviews on the bikes they rent.
- **Bike Autofit Wizard**: An advanced tool that helps users choose the right bike size and type.
- **Document Auto-generation**: Rental agreements and receipts are automatically generated for both the user and the admin.

### PHASE 3: 🛠️ Advanced Management & Analytics
- **Advanced Admin Panel**: Includes detailed reports, stats, and management tools for tracking rentals, users, and revenue.
- **Upcoming Features (TBD)**: Additional features will be planned as the project evolves (suggestions welcome!).


## Technologies 🛠️

This project leverages modern web development technologies and tools:

- **C#** - Core language used for backend development.
- **ASP.NET Core** - For building the backend services.
- **Entity Framework Core** - For database interactions.
- **SQL Server** - Database management system. 



## Installation ⚙️

### Prerequisites
- **.NET 8 SDK**
- **SQL Server**
- **Entity Framework Core .NET Command-line Tools** - Used for applying migrations and managing the database schema. [See docs](https://learn.microsoft.com/en-us/ef/core/cli/dotnet
)
   - You can install it globally
     ```bash
     dotnet tool install --global dotnet-ef
     ```

## Steps to Set Up the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/szczypkakrzys/Bike360.git
   cd Bike360

2. Setup the database
    You need to configure the database connection string in the `appsettings.json` file to point to your local SQL Server instance. Update the connection string as follows:
    ```csharp
    "ConnectionStrings": {
        "Bike360Database": "connectionstring"
    },
    ```
3. Apply migrations to create the database Schema
    ```bash
    dotnet ef database update
    ```
    This command will create the database and apply all migrations based on your EF Core model.
4. Run the application
    ```bash
    dotnet run
    ```
### Future Improvements
A Docker option to simplify the setup process for both the application and the database will be added in the future.


## Features ✨

- **Basic Reservations**: Users can easily browse available bikes and make reservations.
- **Functional UI**: A smooth and responsive user interface to enhance user experience.
- **User Authentication & Roles**: Users can register, log in, and manage their reservations. Admins have additional privileges to manage the platform.
- **Notifications**: Users receive both in-app and email notifications for reservation updates, reminders, etc.
- **Bike Reviews**: Users can leave feedback on bikes they’ve rented.
- **Bike Autofit Wizard**: A guided feature that helps users find the best-fit bike based on their preferences and body measurements.
- **Document Auto-generation**: Automatically generate rental documents for users and admins.
