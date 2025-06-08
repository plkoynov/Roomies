# Domain Layer Implementation

This document outlines the implementation plan for the domain layer of the Hobby Household Expense Tracking System. It includes the entities, their relationships, and junction tables. Each entity's properties are detailed in tables with their data types, constraints, and descriptions. The implementation will use .NET 8 and Entity Framework Core for PostgreSQL as the ORM.

---

## Entities and Relationships

### 1. User
Represents a user in the system.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the user. |
| Name         | string      | No       | 100        | Full name of the user.          |
| Email        | string      | No       | 255        | Email address of the user.      |
| Password     | string      | No       | 255        | Hashed password.                |
| CreatedAt    | DateTime    | No       | -          | Timestamp of user creation.     |

---

### 2. Household
Represents a household in the system.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the household. |
| Name         | string      | No       | 100        | Name of the household.          |
| CreatedBy    | Guid        | No       | -          | User ID of the household creator. |
| CreatedAt    | DateTime    | No       | -          | Timestamp of household creation. |

---

### 3. HouseholdMember
Junction table linking users to households.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the membership. |
| HouseholdId  | Guid        | No       | -          | ID of the household.            |
| UserId       | Guid        | No       | -          | ID of the user.                 |
| MemberType   | MemberType  | No       | -          | Type of membership for the user in the household. |
| JoinedAt     | DateTime    | No       | -          | Timestamp of when the user joined the household. |

---

### 4. Expense
Represents an expense in the system.

| Name           | Data Type   | Nullable | Max Length | Description                      |
|----------------|-------------|----------|------------|----------------------------------|
| Id             | Guid        | No       | -          | Unique identifier for the expense. |
| HouseholdId    | Guid        | No       | -          | ID of the household.            |
| UserId         | Guid        | No       | -          | ID of the user who created the expense. |
| CategoryId     | Guid        | No       | -          | ID of the expense category.     |
| Amount         | decimal(10,2) | No     | -          | Amount of the expense.          |
| Date           | DateTime    | No       | -          | Date of the expense.            |
| Description    | string      | Yes      | 255        | Description of the expense.     |
| OwnershipTypeId| int         | No       | -          | Type of ownership (`household` or `shared`). |
| PaymentStatusId| int         | No       | -          | Payment status of the expense. |
| CreatedAt      | DateTime    | No       | -          | Timestamp of expense creation.  |
| UpdatedAt      | DateTime    | Yes      | -          | Timestamp of last update.       |

---

### 5. ExpenseShare
Represents the sharing details of an expense.

| Name               | Data Type   | Nullable | Max Length | Description                      |
|--------------------|-------------|----------|------------|----------------------------------|
| Id                 | Guid        | No       | -          | Unique identifier for the share. |
| ExpenseId          | Guid        | No       | -          | ID of the expense.              |
| UserId             | Guid        | No       | -          | ID of the user sharing the expense. |
| ShareAmount        | decimal(10,2) | No     | -          | Amount shared by the user.      |
| PaymentStatusId    | int         | No       | -          | Payment status for the share.   |
| StatusUpdatedBy    | Guid        | Yes      | -          | ID of the user who updated the status. |
| StatusUpdatedAt    | DateTime    | Yes      | -          | Timestamp of the status update. |

---

### 6. ExpenseCategories
Represents categories for expenses.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the category. |
| Name         | string      | No       | 100        | Name of the category.           |
| IsDefault    | bool        | No       | -          | Indicates if the category is a default category. |

---

### 7. ExpenseCategoryRelations
Represents hierarchical relationships between categories.

| Name                 | Data Type   | Nullable | Max Length | Description                      |
|----------------------|-------------|----------|------------|----------------------------------|
| AncestorId           | Guid        | No       | -          | ID of the ancestor category.    |
| DescendantId         | Guid        | No       | -          | ID of the descendant category.  |
| Depth                | int         | No       | -          | Depth of the relationship.      |

---

### 8. HouseholdExpenseCategories
Links households to their visible categories.

| Name               | Data Type   | Nullable | Max Length | Description                      |
|--------------------|-------------|----------|------------|----------------------------------|
| HouseholdId        | Guid        | No       | -          | ID of the household.            |
| ExpenseCategoryId  | Guid        | No       | -          | ID of the expense category.     |

---

### 9. Files
Represents files uploaded to the system.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the file. |
| FileName     | string      | No       | 255        | Name of the file.               |
| Content      | byte[]      | No       | -          | Binary content of the file.     |
| Extension    | string      | No       | 10         | File extension (e.g., `.pdf`).  |
| MimeType     | string      | No       | 50         | MIME type of the file.          |
| FileSize     | int         | No       | -          | Size of the file in bytes.      |
| Hash         | string      | No       | 64         | Hash of the file content.       |
| CreatedBy    | Guid        | No       | -          | ID of the user who uploaded the file. |
| CreatedAt    | DateTime    | No       | -          | Timestamp of file upload.       |

---

### 10. ExpenseAttachment
Links files to expenses.

| Name         | Data Type   | Nullable | Max Length | Description                      |
|--------------|-------------|----------|------------|----------------------------------|
| Id           | Guid        | No       | -          | Unique identifier for the attachment. |
| ExpenseId    | Guid        | No       | -          | ID of the expense.              |
| FileId       | Guid        | No       | -          | ID of the file.                 |
| Name         | string      | Yes      | 255        | Display name for the attachment. |

---

## Enums

### 1. OwnershipType
Represents the type of ownership for an expense.

| Name       | Value | Description                     |
|------------|-------|---------------------------------|
| Household  | 1     | Expense is owned by the household. |
| Shared     | 2     | Expense is shared among members. |

---

### 2. PaymentStatus
Represents the payment status of an expense or share.

| Name    | Value | Description                     |
|---------|-------|---------------------------------|
| Pending | 1     | Payment is pending.            |
| Paid    | 2     | Payment has been completed.    |

---

### 3. MemberType
Represents the type of membership for a user in a household.

| Name   | Value | Description                     |
|--------|-------|---------------------------------|
| Admin  | 1     | User is an admin of the household. |
| Member | 2     | User is a regular member of the household. |

---

## Implementation Steps

### 1. Create Abstractions
1. Define `IEntity` interface containing the `Id` property.
2. Define `IAuditableEntity` interface inheriting from `IEntity` and adding `CreatedBy` and `CreatedAt` properties.

### 2. Create Enums
1. Define `OwnershipType` enum with values `Household = 1` and `Shared = 2`.
2. Define `PaymentStatus` enum with values `Pending = 1` and `Paid = 2`.
3. Define `MemberType` enum with values `Admin = 1` and `Member = 2`.

### 3. Create Entity Classes
1. Define `User` entity implementing `IEntity` with the following navigation properties:
   - `ICollection<Household> Households`
   - `ICollection<Expense> Expenses`

2. Define `Household` entity implementing `IAuditableEntity` with the following navigation properties:
   - `ICollection<HouseholdMember> Members`
   - `ICollection<Expense> Expenses`

3. Define `HouseholdMember` entity implementing `IEntity` with the following navigation properties:
   - `Household Household`
   - `User User`

4. Define `Expense` entity implementing `IAuditableEntity` with the following navigation properties:
   - `Household Household`
   - `User User`
   - `ExpenseCategory Category`
   - `ICollection<ExpenseShare> Shares`

5. Define `ExpenseShare` entity implementing `IAuditableEntity` with the following navigation properties:
   - `Expense Expense`
   - `User UpdatedBy`
   - `User Creator`

6. Define `ExpenseCategory` entity implementing `IEntity` with the following navigation properties:
   - `ICollection<Expense> Expenses`
   - `ICollection<ExpenseCategoryRelations> Relations`

7. Define `File` entity implementing `IAuditableEntity` with the following navigation properties:
   - `ICollection<ExpenseAttachment> Attachments`

8. Define `ExpenseAttachment` entity implementing `IEntity` with the following navigation properties:
   - `Expense Expense`
   - `File File`

9. Define `ExpenseCategoryRelations` entity with the following navigation properties:
   - `ExpenseCategory Ancestor`
   - `ExpenseCategory Descendant`

10. Define `HouseholdExpenseCategories` entity with the following navigation properties:
    - `Household Household`
    - `ExpenseCategory ExpenseCategory`