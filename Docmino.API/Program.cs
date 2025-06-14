using Coravel;
using Docmino.API.Filters;
using Docmino.API.Middlewares;
using Docmino.API.OpenApi;
using Docmino.Application;
using Docmino.Infrastructure;
using Docmino.Infrastructure.BackgroundJobs;
using Docmino.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
//Exception handlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Memory Cache
builder.Services.AddMemoryCache();

// APPLICATION LAYER
builder.Services.AddApplication(builder.Configuration);

// INFRASTRUCTURE LAYER
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

//CONTROLERS
builder.Services.AddControllers(config => config.Filters.Add(typeof(ValidateModelAttribute)));

// SWAGGERS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenOptionsConfig.ConfigureSwaggerGenOptions);

var app = builder.Build();
app.UseExceptionHandler();
app.UseMiddleware<JwtMiddleware>();

//add backgroung jobs
app.Services.UseScheduler(scheduler =>
{
    //scheduler.Schedule<DocumentExpirationProcessingJob>().DailyAtHour(12);
    scheduler.Schedule<DocumentExpirationProcessingJob>().EveryMinute();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// This applies any pending migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

//CORS Configuration
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Configuration["FileStorageSettings:Test"] ?? ""),
    RequestPath = app.Configuration["FileStorageSettings:BaseSamplePath"] ?? ""
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(app.Configuration["FileStorageSettings:Path"] ?? ""),
    RequestPath = app.Configuration["FileStorageSettings:BaseFilePath"] ?? ""
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
