using Docmino.Application.Common.Exceptions;
using Docmino.Domain.Abstractions;
using Docmino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Docmino.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public AppDbContext() { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }



    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<SystemMenu> SystemMenus { get; set; }
    public DbSet<SystemFeature> SystemFeatures { get; set; }
    public DbSet<UserFeature> UserFeatures { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentCategory> DocumentCategories { get; set; }
    public DbSet<DocumentField> DocumentFields { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<DocumentRegister> DocumentRegisters { get; set; }
    public DbSet<DocumentFile> DocumentFiles { get; set; }
    public DbSet<ConfirmProcess> ConfirmProcesses { get; set; }
    public DbSet<ProcessDetail> ProcessDetails { get; set; }
    public DbSet<ProcessSignDetail> ProcessSignDetails { get; set; }
    public DbSet<ProcessHistory> ProcessHistories { get; set; }
    public DbSet<ProcessSignHistory> ProcessSignHistories { get; set; }
    public DbSet<DocumentDirectory> DocumentDirectories { get; set; }
    public DbSet<Storage> Storages { get; set; }
    public DbSet<StoragePeriod> StoragePeriods { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
