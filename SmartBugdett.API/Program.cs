using System.Text;
using System.Security.Claims;
using System.Threading.RateLimiting;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartBudgett.Business.Abstract;
using SmartBudgett.Business.Abstract.Services;
using SmartBudgett.Business.Concrete;
using SmartBudgett.Business.Concrete.Managers;
using SmartBudgett.Business.Rules;
using SmartBudgett.Core.Middleware;
using SmartBudgett.Core.Security.Abstract;
using SmartBudgett.Core.Security.Concrete;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.DataAccess.Concrete;
using SmartBudgett.DataAccess.Context;
using SmartBudgett.API.Mapping;
using SmartBudgett.DTO.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

var jwtSecurityKey = builder.Configuration["Jwt:SecurityKey"];

if (string.IsNullOrWhiteSpace(jwtSecurityKey) ||
    Encoding.UTF8.GetByteCount(jwtSecurityKey) < 32)
{
    throw new InvalidOperationException(
        "Jwt:SecurityKey yapılandırılmamış veya çok kısa. En az 32 baytlık bir anahtarı User Secrets ya da ortam değişkeniyle sağlayın.");
}

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();


// DbContext
builder.Services.AddDbContext<SmartBudgetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ExpenseCreateDtoValidator>();

// AutoMapper
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(CategoryMappingProfile),
    typeof(ExpenseMappingProfile),
    typeof(IncomeMappingProfile));

builder.Services.AddControllers();

// Repositories (DAL)
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Unit of Work Pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services (Business)
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IIncomeService, IncomeManager>();
builder.Services.AddScoped<IExpenseService, ExpenseManager>();

// Business Rules
builder.Services.AddScoped<AuthBusinessRule>();
builder.Services.AddScoped<CategoryBusinessRules>();
builder.Services.AddScoped<ExpenceBusinessRules>();
builder.Services.AddScoped<IncomeBusinessRules>();
builder.Services.AddScoped<UserBusinessRule>();

// Security & JWT
builder.Services.AddScoped<ITokenHelper, JwtHelper>();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    });
});
builder.Services.AddHttpClient<IAiService, AiManager>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("ai-analysis", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? context.Connection.RemoteIpAddress?.ToString()
                ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
});
// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
        };
    });

builder.Services.AddAuthorization();
// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartBudgett API", Version = "v1", Description = "Budget Management API with async operations" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowedOrigins");
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers();

app.Run();
