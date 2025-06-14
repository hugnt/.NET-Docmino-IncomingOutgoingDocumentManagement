using Coravel;
using Docmino.Application.Abstractions.Authentication;
using Docmino.Application.Abstractions.Email;
using Docmino.Application.Abstractions.FileSignature;
using Docmino.Application.Abstractions.FileStorage;
using Docmino.Application.Abstractions.HostedServices;
using Docmino.Application.Abstractions.HttpContext;
using Docmino.Application.Models.External.Providers;
using Docmino.Infrastructure.Authentication;
using Docmino.Infrastructure.BackgroundJobs;
using Docmino.Infrastructure.Email;
using Docmino.Infrastructure.FileSignature;
using Docmino.Infrastructure.FileStorage;
using Docmino.Infrastructure.HostedServices;
using Docmino.Infrastructure.HttpContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;
using System.Reflection;

namespace Docmino.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Access context
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        //Token & Authentication
        services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
        var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = TokenValidationConfig.GetTokenValidationParameters(tokenSettings!);
        });
        services.AddSingleton<IAuthenticationService, AuthenticationService>();

        //Background services
        services.AddSingleton(typeof(IBackgroundTaskQueue<>), typeof(BackgroundTaskQueue<>));
        services.AddHostedService<MailSenderBackgroundService>();

        //Email
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();
        services.AddSingleton<IRazorLightEngine>(provider =>
            new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetExecutingAssembly(), "Docmino.Infrastructure")
                .UseMemoryCachingProvider()
                .Build()
        );

        //File storage - local
        //services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));
        //services.AddTransient<IFileStorageService, FileStorageService>();

        //File storage - cloudinary
        services.Configure<CloudSettings>(configuration.GetSection("CloudinarySettings"));
        services.AddTransient<IFileStorageService, CloudinaryStorageService>();

        //File - Signature 
        services.AddHttpClient();
        services.AddScoped<IFileSignatureService, FileSignatureService>();

        //Background Job
        services.AddScheduler();
        services.AddTransient<DocumentExpirationProcessingJob>();

        return services;
    }
}
