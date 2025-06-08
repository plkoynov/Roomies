# Detailed Step-by-Step Implementation Plan

## Overview

This plan splits the implementation by feature and by backend (BE) and frontend (FE), starting with the backend. Each feature is broken down into well-defined, actionable steps to facilitate development, testing, and integration.

---

## 1. Backend (BE) Implementation Plan

### 1.1. Project Setup & Architecture

1. **Initialize Solution and Projects**
   - Create a new solution with projects: Domain, Application, Persistence, Presentation (API).
   - Set up folder structure according to vertical slice and clean architecture.

2. **Configure Core Libraries**
   - Add and configure EF Core for PostgreSQL.
   - Add AutoMapper for DTO mappings.
   - Add FluentValidation for request validation.
   - Add JWT authentication (ASP.NET Core Identity or custom).
   - Set up dependency injection and logging.
   - Implement environment-specific configuration files.

3. **Setup Database**
   - Design initial migrations for Users, Households, Categories, Expenses, Files, Attachments, and related tables.
   - Apply and test migrations.

---

### 1.2. User Management

1. **User Registration & Authentication**
   - Implement registration endpoint (with hashed password storage).
   - Implement login endpoint (issue JWT).
   - Implement middleware to validate JWT on protected endpoints.
   - Add user model, repository, and service.

2. **User CRUD & Household Membership**
   - Implement endpoints to retrieve/update user info (except password change, which can be deferred).
   - Implement join/leave household logic and endpoints.

---

### 1.3. Household Management

1. **Household CRUD**
   - Implement create household endpoint.
   - Implement endpoints for listing/joining/leaving households.
   - Implement logic for admin assignment (created_by).
   - Implement invitation system (by email or code).
   - Implement remove member endpoint (admin only).
   - Add household repository and service.

2. **Admin Role Enforcement**
   - Enforce single-admin logic at service layer.
   - Add middleware or attribute-based checks for admin-protected routes.

---

### 1.4. Category Management

1. **ExpenseCategories, Category Relations, Household Categories**
   - Implement ExpenseCategories nomenclature table and default data seeding.
   - Implement ExpenseCategoryRelations (closure table) logic for category hierarchy.
   - Implement HouseholdExpenseCategories for category visibility in each household.
   - Implement endpoints for listing and managing categories (admin only for create/hide/show).
   - Provide endpoints to retrieve usable categories for each household.

---

### 1.5. Expense Management

1. **Expense CRUD**
   - Implement endpoints for create, read, update, delete expense.
   - Add support for both household-wide and shared expense types.
   - Enforce ownership and access rules.
   - Implement validation for fields and logic for splits.

2. **ExpenseShare Logic**
   - Implement creation and management of ExpenseShare records for shared expenses.
   - Implement per-user payment status, updatable only by creator.
   - Track status change history (user, timestamp).

3. **Expense Reporting Endpoints**
   - Add endpoints for listing/filtering expenses by category, date, user, payment status, etc.

---

### 1.6. Attachment & File Management

1. **Files and Attachments**
   - Implement file upload endpoint with file type/size validation.
   - Compute/store hash for deduplication.
   - Store file as blob and metadata in Files table.
   - Link files to expenses via ExpenseAttachment table (with display name).
   - Implement download/view/delete endpoints for attachments.
   - Enforce attachment count per expense.

2. **Attachment Deduplication**
   - On upload, check hash to see if file content already exists; link if duplicate.

---

### 1.7. Dashboard & Reporting

1. **Reporting Endpoints**
   - Implement endpoints to fetch:
     - Pie chart data (category, subcategory, current month)
     - Line chart data (monthly totals for previous 12 months)
     - Table data for pending payments (with required columns only)
   - Optimize queries for performance.

---

### 1.8. Security & Environment

1. **Finalize Security**
   - Ensure all endpoints (except auth) are JWT-protected.
   - Review and harden security (input validation, error handling, etc.).
   - Implement audit logging for sensitive actions.

2. **Configuration Management**
   - Finalize and document environment-specific configuration files.
   - Set up dev/prod database connections, secrets, and file storage settings.

---

### 1.9. Testing

1. **Unit & Integration Tests**
   - Write unit tests for services, especially business logic.
   - Write integration tests for main API endpoints.
   - Test file uploads, deduplication, and attachment logic.

---

## 2. Frontend (FE) Implementation Plan

### 2.1. Project Setup

1. **Initialize Angular Project**
   - Create new Angular workspace.
   - Add Angular Material, Flex Layout, and other dependencies.
   - Set up environment config files.

2. **App Structure**
   - Define core modules: Auth, Dashboard, Household, Expenses, Categories, Attachments, Shared.

---

### 2.2. Authentication & User Management

1. **User Registration/Login**
   - Build registration and login forms.
   - Implement JWT storage and HTTP interceptor.
   - Add user context service for current user state.

2. **User Profile & Household Membership**
   - Implement profile page (basic info).
   - Household membership join/leave UI.

---

### 2.3. Household Management

1. **Household CRUD & Admin Actions**
   - UI for creating, joining, leaving households.
   - Admin-only UI for inviting/removing members.
   - Display of current household, members, and roles.

---

### 2.4. Category Management

1. **Category Browsing/Management**
   - Tree or list UI for categories and subcategories.
   - Admin-only screens for adding/hiding/showing categories.
   - Visual distinction for default vs. custom categories.

---

### 2.5. Expense Management

1. **Expense CRUD**
   - Forms for adding/editing expenses (with ownership type selection).
   - UI for splitting shared expenses (member picker, split input).
   - Display expense list, filter/sort by category, member, payment status.
   - Show per-user payment state for shared expenses.
   - Only allow creators to change payment statuses.

---

### 2.6. Attachment & File Management

1. **Upload/Attach Files**
   - UI for uploading files (drag-drop or file picker), enforcing type/count/size restrictions.
   - Display attached files with download/view/delete options.
   - Show warning or info if a file with the same content already exists (deduplication).

---

### 2.7. Dashboard & Reporting

1. **Visualization Components**
   - Pie chart of expenses by category (current month).
   - Pie chart by subcategory (current month, more detailed).
   - Line chart of monthly expenses over past 12 months.
   - Table of all pending payments (columns: expense, category, amount, date, responsible member(s)).

---

### 2.8. Security, Error Handling, and Testing

1. **Access Control**
   - Ensure only authorized users can access protected pages.
   - Hide admin-only actions from non-admins.

2. **Error Handling**
   - Show user-friendly errors for failed actions or validation issues.

3. **Testing**
   - Write unit and integration tests for main components and services.

---

## 3. Final Integration & UAT

1. **End-to-End Testing**
   - Connect FE to BE in dev environment.
   - Test all flows: registration, household creation, expense CRUD, attachments, visualization.
   - User acceptance testing with test users.

2. **Performance & Security Review**
   - Run performance tests on dashboard/reporting endpoints.
   - Review for any missing access controls or data leaks.

3. **Prepare for Deployment**
   - Clean up code, finalize documentation, prepare deployment scripts (if needed).

---

**You can use this plan as a master checklist for implementation. Each bolded step can be split into granular tasks/tickets for tracking.**