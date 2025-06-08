# Login Endpoint Implementation Plan

## Overview

This document outlines the step-by-step implementation plan for the login functionality in the backend (BE) of the Hobby Household Expense Tracking System. The login endpoint will authenticate users and issue a JWT token for secure access to protected resources.

---

## 2. Implementation Steps

### 2.1. Domain Layer

1. **Add Login DTOs**
   - Create `LoginRequestDto` with fields:
     - `Email` (string, required)
     - `Password` (string, required)
   - Create `LoginResponseDto` with fields:
     - `Token` (string)
     - `UserId` (GUID)
     - `Name` (string)

---

### 2.2. Application Layer

1. **Add Login Service**
   - Create a `LoginService` class in the `Services` folder.
   - Implement the following method:
     ```csharp
     Task<ServiceResponse<LoginResponseDto>> AuthenticateUser(LoginRequestDto request);
     ```
   - Logic:
     - Validate the user exists by email.
     - Verify the password using a hashing algorithm.
     - Generate a JWT token if authentication succeeds.

2. **Add Unit Tests**
   - Write unit tests for `LoginService` to cover:
     - Valid login.
     - Invalid email.
     - Invalid password.

---

### 2.3. Persistence Layer

1. **Add User Repository Method**
   - Extend the `IUserRepository` interface to include:
     ```csharp
     Task<User> GetUserByEmailAsync(string email);
     ```
   - Implement the method in the `UserRepository` class.

---

### 2.4. Presentation Layer

1. **Add Login Controller**
   - Create a `LoginController` in the `Controllers` folder.
   - Add the following endpoint:
     ```csharp
     [HttpPost("api/auth/login")]
     public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
     ```
   - Logic:
     - Call `LoginService.AuthenticateUser`.
     - Return `200 OK` with the token on success.
     - Return `401 Unauthorized` on failure.

2. **Add Validation**
   - Use `FluentValidation` to validate `LoginRequestDto`.
   - Ensure `Email` is a valid email format and `Password` is not empty.

---

## 3. API Documentation

### Login Endpoint

**URL**: `/api/auth/login`

**Method**: `POST`

**Request Body**:
```json
{
  "email": "string",
  "password": "string"
}
```

**Response**:
- **200 OK**:
  ```json
  {
    "token": "string",
    "userId": "string",
    "name": "string"
  }
  ```
- **401 Unauthorized**:
  ```json
  {
    "error": "Invalid credentials."
  }
  ```

**Description**: Authenticates a user and returns a JWT token upon successful login. If authentication fails, an error message is returned.

---

## 4. Security

1. **Password Hashing**
   - Use a secure hashing algorithm (e.g., `BCrypt`) to verify passwords.

2. **JWT Token**
   - Include the following claims in the token:
     - `sub` (User ID)
     - `name` (User Name)
     - `email` (User Email)
   - Set token expiration to 1 hour.
   - Sign the token using a secure key stored in environment variables.
   - Read the token secret from the configuration files instead of hardcoding it.

---

## 5. Testing

1. **Unit Tests**
   - Cover all service and repository methods.

---

## 6. Deployment

1. **Documentation**
   - Update API documentation to include the login endpoint.

---

## 7. Checklist

- [x] DTOs created.
- [x] Login service implemented and tested.
- [x] Repository method added.
- [x] Controller and endpoint implemented.
- [x] Validation and security measures in place.
- [x] Tests written and passed.
- [x] Documentation updated.
