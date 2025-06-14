using Docmino.Domain.Abstractions;
using Docmino.Persistence.Interceptors;
using Docmino.Persistence.Repositories;
using Docmino.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docmino.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //db
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddDbContext<AppDbContext>((sp,options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database")!);
            options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());
        });

        //Unit of work
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        //Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IDocumentRepository, DocumentRepository>();

        return services;
    }
}
