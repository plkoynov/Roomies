using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Roomies.Application.Interfaces;
using Roomies.Application.Interfaces.Services;
using Roomies.Domain.Interfaces;

namespace Roomies.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoomiesDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWork(RoomiesDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task SaveChangesAsync()
        {
            var currentUser = _currentUserService.GetCurrentUser();

            var addedEntities = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is IAuditableEntity)
                .Select(e => e.Entity as IAuditableEntity);

            foreach (var entity in addedEntities)
            {
                entity.CreatedBy = currentUser.Id;
                entity.CreatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
