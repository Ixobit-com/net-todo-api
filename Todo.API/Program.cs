using System.Text.Json.Serialization;
using Todo.API.Configurations;
using Todo.API.Constants;
using Todo.API.Filters;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure database
builder.Services.ConfigureDatabase(connectionString);

// Configure identity
builder.Services.ConfigureIdentity();

builder.Services
    .AddControllers(options => {
        options.Filters.Add(new LogRequestActionFilterAttribute());
        //options.InputFormatters.Insert();
    })
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

// Configure cors
builder.Services.AddCors(options => {
    options.AddPolicy(PolicyNames.Cors, builder => {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

// Configure swagger
builder.Services.ConfigureSwagger();

// Configure authentication
builder.Services.ConfigureAuthentication(builder.Configuration);

// Configure authorization
builder.Services.ConfigureAuthorization();

// Configure auto mapper
builder.Services.ConfigureAutoMapper();

// Configure validators
builder.Services.ConfigureValidators();

// Configure services
builder.Services.ConfigureServices();

// Configure logging
builder.Services.ConfigureLogging(builder.Logging, builder.Host);

// Configure client hasher
builder.Services.ConfigureClientHasher(builder.Configuration);

// Configure SMTP
builder.Services.ConfigureSmtp(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Apply migrations
app.ApplyMigrations();

// Seed data
await app.SeedAsync(builder.Configuration);

app.UseForwardedHeaders();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(PolicyNames.Cors);

app.UseAuthorization();

app.MapControllers();

app.Run();