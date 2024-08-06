using Library.Api.Domain.Authors.Services;
using Library.Api.Domain.Authors.Services.Interfaces;
using Library.Api.Domain.Books.Services;
using Library.Api.Domain.Books.Services.Interfaces;
using Library.Api.Options;
using Library.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

// Services
builder.Services.AddSingleton<IAuthorsService, AuthorsService>();
builder.Services.AddSingleton<IBooksService, BooksService>();

builder.Services.AddDbContext<LibraryDbContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
{
    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

    dbContextOptionsBuilder.UseNpgsql(databaseOptions.ConnectionString, sqlServerAction =>
    {
        sqlServerAction.MigrationsAssembly("Library.Database");
        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
    });

    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
}, ServiceLifetime.Singleton);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
