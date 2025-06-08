# Extending UnitOfWork

## Step-by-Step Checklist

1. **Inject `ICurrentUserService` into `UnitOfWork`**:
   - Add a private readonly field for `ICurrentUserService`.
   - Modify the constructor of `UnitOfWork` to accept an `ICurrentUserService` parameter and initialize the field.

2. **Update `SaveChangesAsync` Method**:
   - Retrieve the current user ID from `ICurrentUserService`.
   - Iterate over all tracked entities in the `RoomiesDbContext` that are in the `Added` state.
   - Check if the entity implements the `IAuditableEntity` interface.
   - If it does, set the `CreatedBy` field to the current user ID and the `CreatedOn` field to the current UTC time.
