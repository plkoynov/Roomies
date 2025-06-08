# Registration Endpoint Implementation Checklist

## Steps to Implement Registration Endpoint

- [x] **Service Layer**
  - [x] Create a `IUserService` interface in a separate `Interfaces` folder within the Application project.
  - [x] Create a `UserService` class to encapsulate user-related business logic and implement the `IUserService` interface.
  - [x] Implement a method `RegisterUser` in the service to handle the registration process.
    - **Input Parameters:**
      - [x] Accept a `RegisterUserDto` object containing:
        - `Name` (string): The full name of the user. Must not be null or empty and have a maximum length of 100 characters.
        - `Email` (string): The email address of the user. Must be a valid email format and have a maximum length of 255 characters.
        - `Password` (string): The plain-text password provided by the user. Must not be null or empty and have a minimum length of 8 characters.
    - [x] Validate the input data (e.g., check for null or invalid fields).
    - [x] Check if the email already exists in the database using the `IUserRepository`.
    - [x] Hash the password using a secure hashing algorithm (e.g., BCrypt).
    - [x] Save the new user to the database using the `UnitOfWork` pattern.
      - Use `IUserRepository` for user-specific operations.
      - Commit the transaction using `IUnitOfWork`.
  - [x] Add an extension method in the Application project to register `UserService` for dependency injection, similar to how it is done in `PersistanceExtensions.cs`.

- [x] **Controller and Endpoint**
  - [x] Create a `POST /api/auth/register` endpoint in the API controller.
  - [x] Accept a `RegisterUserRequest` model in the request body containing:
    - `Name` (string): The full name of the user. Must not be null or empty and have a maximum length of 100 characters.
    - `Email` (string): The email address of the user. Must be a valid email format and have a maximum length of 255 characters.
    - `Password` (string): The plain-text password provided by the user. Must not be null or empty, have a minimum length of 8 characters, and include at least one lowercase letter, one uppercase letter, one number, and one special character.
  - [x] Validate the `RegisterUserRequest` model using FluentValidation.
    - Add a rule for `Password` to enforce strong password requirements using a regular expression: `^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$`.
  - [x] Add a step to create a `RegisterUserRequestValidator` class for FluentValidation.
  - [x] Map the `RegisterUserRequest` model to a `RegisterUserDto` and pass it to the service layer.

- [x] **Response Model**
  - [x] Create a `RegisterUserResponse` model to return upon successful registration.
    - Include fields such as `UserId` (GUID) and `Token` (string).
  - [x] Map the service layer result to the `RegisterUserResponse` model and return it in the response.

- [x] **JWT Integration**
  - [x] Generate a JWT token upon successful registration.
  - [x] Return the token in the response.

- [x] **Unit Tests**
  - [x] Write unit tests for the service layer to ensure password hashing and duplicate email checks work correctly.

- [ ] **Documentation**
  - [x] Document the registration endpoint in the API documentation.
  - [x] Include request/response examples and validation rules.

### Registration Endpoint Documentation

**Endpoint:** `POST /api/auth/register`

**Description:** Registers a new user and returns a JWT token upon successful registration.

**Request Body:**
```json
{
  "name": "string (max 100 characters, required)",
  "email": "string (valid email format, max 255 characters, required)",
  "password": "string (min 8 characters, must include at least one lowercase letter, one uppercase letter, one number, and one special character, required)"
}
```

**Response:**
- **Success (200 OK):**
```json
{
  "token": "string (JWT token)"
}
```
- **Error (400 Bad Request):**
```json
{
  "errors": [
    "string (validation error messages)"
  ]
}
```

**Validation Rules:**
- `Name`: Required, max length 100 characters.
- `Email`: Required, valid email format, max length 255 characters.
- `Password`: Required, min length 8 characters, must include at least one lowercase letter, one uppercase letter, one number, and one special character.

**Notes:**
- Ensure the email is unique.
- Passwords are securely hashed before storage.

## Notes
Refer to the PRD for additional details on user registration requirements and constraints.
