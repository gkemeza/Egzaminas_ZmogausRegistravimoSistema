# Human registration system

- A full-stack web application built with ASP.NET Core for human registration and management system.
- The system features a RESTful Web API backend with a responsive frontend interface for complete user management operations.

## Overview

This application enables organizations to register, manage, and track human records through a user-friendly web interface backed by an API. 
It provides complete CRUD operations with validation, authentication, and documentation.

## Features:
- RESTful Web API endpoints
- Models, controllers, and routes
- CRUD operations
- Integrated with a database using Entity Framework Core
- Authentication and authorization
- Error handling and validation
- Unit testing
- OpenAPI documentation
- Responsive frontend design

## Technologies Used:  

 **Backend**
  - Framework: ASP.NET Core
  - Language: C#
  - ORM: Entity Framework Core
  - Database: SQL Server
  - Testing: xUnit
  - Documentation: Swagger/OpenAPI
    
**Frontend**
  - Structure: HTML5
  - Styling: CSS3
  - Interactivity: JavaScript
    
## API Endpoints

**The API provides the following main endpoints:**

- **GET** /api/User/{id} - Retrieve a specific user
- **POST** /api/User/SignUp - Register a new user
  - /api/User/Login - User login
- **PUT** /api/Profile/UpdateUsername - Update user username
  -  /api/Profile/UpdatePassword - Update user password
  -  /api/Profile/UpdateEmail - Update user email
  -  and many others
- **DELETE** /api/User/{id} - Delete a user (only available to 'admin' role)
