using Docmino.Application.Abstractions.HttpContext;
using Docmino.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Docmino.Persistence.Interceptors;
public class AuditableEntityInterceptor(IUserContext userContext) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedBy).CurrentValue = userContext.UserId;
                entityEntry.Property(a => a.CreatedAt).CurrentValue = DateTime.Now;
                //entityEntry.Property(a => a.CreatedAt).CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.UpdatedBy).CurrentValue = userContext.UserId;
                entityEntry.Property(a => a.UpdatedAt).CurrentValue = DateTime.Now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}

