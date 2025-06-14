using Docmino.Application.Handlers;
using Docmino.Application.Processors;
using Docmino.Application.Processors.Implement;
using Docmino.Application.Services;
using Docmino.Application.Services.Implement;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docmino.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Handlers
        services.AddTransient<IFileHandler, FileHandler>();

        // Fluent validation
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();


        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IDocumentFileService, DocumentFileService>();
        services.AddScoped<IExternalDocumentService, ExternalDocumentService>();
        services.AddScoped<IInternalDocumentService, InternalDocumentService>();
        services.AddScoped<IConfirmProcessService, ConfirmProcessService>();

        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IGroupService, GroupService>();

        services.AddScoped<IStoragePeriodService, StoragePeriodService>();
        services.AddScoped<IDocumentRegisterService, DocumentRegisterService>();
        services.AddScoped<IDocumentCategoryService, DocumentCategoryService>();
        services.AddScoped<IDocumentFieldService, DocumentFieldService>();
        services.AddScoped<IOrganizationService, OrganizationService>();


        services.AddScoped<IDocumentDirectoryService, DocumentDirectoryService>();
        services.AddScoped<IStorageService, StorageService>();


        services.AddScoped<IStatisticService, StatisticService>();

        //Processors
        services.AddScoped<IDocumentProcessor, DocumentProcessor>();

        return services;
    }
}
