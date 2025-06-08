# User Details Endpoint Checklist

This checklist outlines the steps required to implement an endpoint for retrieving the logged-in user's information (Name and Email) by their provided ID. Follow these steps to ensure a complete implementation.

---

## 1. Application Layer

### 1.1. Create a DTO for User Details
- [x] Create a file `Roomies.Application/Models/UserDetailsDto.cs`.
- [x] Define a class `UserDetailsDto` with properties:
  - [x] `Name` (string)
  - [x] `Email` (string)

### 1.2. Add a Service Method to Retrieve User Details
- [x] Open `Roomies.Application/Services/UserService.cs`.
- [x] Add a method `GetUserDetailsByIdAsync`:
  - [x] Accepts a user ID as input.
  - [x] Returns a `UserDetailsDto`.
  - [x] Uses `IUserRepository` to query the database for the user.
  - [x] Maps the user entity to `UserDetailsDto`.

### 1.3. Introduce CurrentUserService
- [x] Create a new service `CurrentUserService` in `Roomies.Application/Services/CurrentUserService.cs`.
- [x] Create an interface `ICurrentUserService` in `Roomies.Application/Interfaces/Services/ICurrentUserService.cs`.
- [x] Ensure `CurrentUserService` implements `ICurrentUserService`.
- [x] Add a method `GetCurrentUserAsync`:
  - [x] Retrieves the currently logged-in user's information from the context.
  - [x] Returns a `UserDetailsDto` with the user's ID, Name, and Email.
- [x] Ensure the service is registered in the dependency injection container.

---

## 2. Persistence Layer

### 2.1. Add a Repository Method
- [x] Open `Roomies.Persistence/Repositories/UserRepository.cs`.
- [x] Add a method `GetUserByIdAsync`:
  - [x] Queries the database for a user by their ID.
  - [x] Returns the user entity.

---

## 3. Presentation Layer

### 3.1. Add a Controller Endpoint
- [x] Open `Roomies.Presentation/Controllers/UserController.cs`.
- [x] Add a new action `GetUserDetails`:
  - [x] Accepts a user ID as a route parameter.
  - [x] Calls `UserService.GetUserDetailsByIdAsync`.
  - [x] Returns the result as an HTTP 200 response if the user is found.
  - [x] Returns an HTTP 404 response if no user is found with the given ID.

#### Documentation
- **Endpoint**: `GET /api/users/{id}`
- **Authorization**: Requires a valid JWT token.
- **Description**: Retrieves the details (Name and Email) of a user by their ID.
- **Responses**:
  - `200 OK`: Returns the user details in the response body.
  - `404 Not Found`: If no user is found with the given ID.

---

## 4. Testing Layer

### 4.1. Add Unit Tests for the Service
- [x] Open `Roomies.Application.Tests/Services/UserServiceTests.cs`.
- [x] Add tests for `GetUserDetailsByIdAsync`:
  - [x] Mock `IUserRepository` to return a user entity.
  - [x] Verify correct mapping to `UserDetailsDto`.

---

## Summary

Use this checklist to track progress while implementing the User Details endpoint. Ensure proper testing and validation at each step to maintain code quality and reliability.
