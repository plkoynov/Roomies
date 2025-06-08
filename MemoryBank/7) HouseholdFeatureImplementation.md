# Household Feature Implementation Checklist

## Overview
This checklist outlines the steps required to implement the household management feature, focusing on four endpoints: creating, deleting, editing, and listing households for the logged-in user. The domain layer is already complete, so this implementation will focus on the application, persistence, and presentation layers.

---

## Checklist

### 1. Backend Implementation

#### 1.1. Create Household Endpoint
- [x] Add a new method in the `IHouseholdRepository` interface for adding a household.
- [x] Implement the method in the `HouseholdRepository` class.
- [x] Add a service method in `HouseholdService` to handle business logic for creating a household.
- [x] Create a request model `CreateHouseholdRequest` for the controller.
- [x] Add a new API endpoint in the `HouseholdController` to handle the creation request.
- [x] Write unit tests for the service method.

#### 1.2. Delete Household Endpoint
- [ ] Add a new method in the `IHouseholdRepository` interface for deleting a household by ID.
- [ ] Implement the method in the `HouseholdRepository` class.
- [ ] Add a service method in `HouseholdService` to handle business logic for deleting a household.
- [ ] Create a request model `DeleteHouseholdRequest` and a response model `DeleteHouseholdResponse` for the controller.
- [ ] Map the request and response models to service DTOs.
- [ ] Ensure the service method returns a `ServiceResponse<DeleteHouseholdResponseDto>`.
- [ ] Add a new API endpoint in the `HouseholdController` to handle the delete request.
- [ ] Write unit tests for the service method.
- [ ] Write integration tests for the API endpoint.

#### 1.3. Edit Household Endpoint
- [ ] Add a new method in the `IHouseholdRepository` interface for updating household details.
- [ ] Implement the method in the `HouseholdRepository` class.
- [ ] Add a service method in `HouseholdService` to handle business logic for editing a household.
- [ ] Create a request model `EditHouseholdRequest` and a response model `EditHouseholdResponse` for the controller.
- [ ] Map the request and response models to service DTOs.
- [ ] Ensure the service method returns a `ServiceResponse<EditHouseholdResponseDto>`.
- [ ] Add a new API endpoint in the `HouseholdController` to handle the edit request.
- [ ] Write unit tests for the service method.
- [ ] Write integration tests for the API endpoint.

#### 1.4. List Households Endpoint
- [ ] Add a new method in the `IHouseholdRepository` interface for retrieving households by user ID.
- [ ] Implement the method in the `HouseholdRepository` class.
- [ ] Add a service method in `HouseholdService` to handle business logic for listing households.
- [ ] Create a response model `HouseholdItemResponse` for the controller.
- [ ] Ensure the service method returns a `ServiceResponse<List<HouseholdItemResponse>>`.
- [ ] Add a new API endpoint in the `HouseholdController` to handle the list request.
- [ ] Write unit tests for the service method.
- [ ] Write integration tests for the API endpoint.

---

### 2. Final Testing and Integration
- [ ] Write unit tests for all service methods in `HouseholdService`.
- [ ] Validate that only authorized users can perform actions on households.
- [ ] Ensure proper error handling and user-friendly error messages.
- [ ] Optimize database queries for performance.

---

This checklist serves as a guide for implementing the household management feature. Each step can be tracked as a separate task or ticket in the project management system.
