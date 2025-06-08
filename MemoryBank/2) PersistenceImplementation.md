# Persistence Layer Implementation Checklist

This document outlines the step-by-step actions required to map the entities defined in the Domain layer to database tables using Entity Framework Core. Each step ensures that the constraints and max lengths specified in the Domain layer are respected.

---

## Checklist for Persistence Layer Implementation

### 1. Configure Entity Framework Core
- [x] Install EF Core NuGet packages for PostgreSQL.
- [x] Add `RoomiesDbContext` class to the Persistence project.
- [x] Configure `RoomiesDbContext` to include DbSet properties for all entities.

### 2. Map Entities to Tables

#### 2.1. User Entity
- [x] Create a `UserConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<User>`.
- [x] Map `Id` as the primary key.
- [x] Set `Name` with a max length of 100 and required.
- [x] Set `Email` with a max length of 255 and required.
- [x] Set `Password` with a max length of 255 and required.
- [x] Configure `CreatedAt` as required.
- [x] Define a one-to-many relationship with `Household` (User can create multiple households).
- [x] Define a one-to-many relationship with `Expense` (User can create multiple expenses).

#### 2.2. Household Entity
- [x] Create a `HouseholdConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<Household>`.
- [x] Map `Id` as the primary key.
- [x] Set `Name` with a max length of 100 and required.
- [x] Configure `CreatedBy` as a required foreign key referencing `User`.
- [x] Configure `CreatedAt` as required.
- [x] Define a one-to-many relationship with `HouseholdMember` (Household can have multiple members).
- [x] Define a one-to-many relationship with `Expense` (Household can have multiple expenses).

#### 2.3. HouseholdMember Entity
- [x] Create a `HouseholdMemberConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<HouseholdMember>`.
- [x] Map `Id` as the primary key.
- [x] Configure `HouseholdId` as a required foreign key referencing `Household`.
- [x] Configure `UserId` as a required foreign key referencing `User`.
- [x] Configure `MemberType` as required.
- [x] Configure `JoinedAt` as required.
- [x] Define a many-to-many relationship between `User` and `Household` through `HouseholdMember`.

#### 2.4. Expense Entity
- [x] Create an `ExpenseConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<Expense>`.
- [x] Map `Id` as the primary key.
- [x] Configure `HouseholdId` as a required foreign key referencing `Household`.
- [x] Configure `UserId` as a required foreign key referencing `User`.
- [x] Configure `CategoryId` as a required foreign key referencing `ExpenseCategory`.
- [x] Set `Amount` as required with precision (10,2).
- [x] Configure `Date` as required.
- [x] Set `Description` with a max length of 255 and optional.
- [x] Configure `OwnershipTypeId` and `PaymentStatusId` as required.
- [x] Configure `CreatedAt` as required and `UpdatedAt` as optional.
- [x] Define a one-to-many relationship with `ExpenseShare` (Expense can have multiple shares).
- [x] Define a one-to-many relationship with `ExpenseAttachment` (Expense can have multiple attachments).

#### 2.5. ExpenseShare Entity
- [x] Create an `ExpenseShareConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<ExpenseShare>`.
- [x] Map `Id` as the primary key.
- [x] Configure `ExpenseId` as a required foreign key referencing `Expense`.
- [x] Configure `UserId` as a required foreign key referencing `User`.
- [x] Set `ShareAmount` as required with precision (10,2).
- [x] Configure `PaymentStatusId` as required.
- [x] Configure `StatusUpdatedBy` as an optional foreign key referencing `User`.
- [x] Configure `StatusUpdatedAt` as optional.

#### 2.6. ExpenseCategory Entity
- [x] Create an `ExpenseCategoryConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<ExpenseCategory>`.
- [x] Map `Id` as the primary key.
- [x] Set `Name` with a max length of 100 and required.
- [x] Configure `IsDefault` as required.
- [x] Define a one-to-many relationship with `Expense` (Category can have multiple expenses).

#### 2.7. ExpenseCategoryRelations Entity
- [x] Create an `ExpenseCategoryRelationsConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<ExpenseCategoryRelations>`.
- [x] Configure `AncestorId` and `DescendantId` as a composite primary key.
- [x] Configure `AncestorId` as a required foreign key referencing `ExpenseCategory`.
- [x] Configure `DescendantId` as a required foreign key referencing `ExpenseCategory`.
- [x] Configure `Depth` as required.

#### 2.8. HouseholdExpenseCategories Entity
- [x] Create a `HouseholdExpenseCategoriesConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<HouseholdExpenseCategories>`.
- [x] Configure `HouseholdId` and `ExpenseCategoryId` as a composite primary key.
- [x] Configure `HouseholdId` as a required foreign key referencing `Household`.
- [x] Configure `ExpenseCategoryId` as a required foreign key referencing `ExpenseCategory`.

#### 2.9. File Entity
- [x] Create a `FileConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<File>`.
- [x] Map `Id` as the primary key.
- [x] Set `FileName` with a max length of 255 and required.
- [x] Configure `Content` as required.
- [x] Set `Extension` with a max length of 10 and required.
- [x] Set `MimeType` with a max length of 50 and required.
- [x] Configure `FileSize` as required.
- [x] Set `Hash` with a max length of 64 and required.
- [x] Configure `CreatedBy` as a required foreign key referencing `User`.
- [x] Configure `CreatedAt` as required.
- [x] Define a one-to-many relationship with `ExpenseAttachment` (File can have multiple attachments).

#### 2.10. ExpenseAttachment Entity
- [x] Create an `ExpenseAttachmentConfiguration` class in a `Configurations` folder implementing `IEntityTypeConfiguration<ExpenseAttachment>`.
- [x] Map `Id` as the primary key.
- [x] Configure `ExpenseId` as a required foreign key referencing `Expense`.
- [x] Configure `FileId` as a required foreign key referencing `File`.
- [x] Set `Name` with a max length of 255 and optional.

### 3. Apply Configurations
- [x] Use `ModelBuilder` in `DbContext` to apply all configurations.

### 4. Migrations
- [x] Generate initial migrations for all entities.
- [x] Apply migrations to the database.
- [x] Verify the database schema matches the entity definitions.

---

This checklist ensures that the persistence layer is implemented correctly and adheres to the constraints and relationships defined in the Domain layer.