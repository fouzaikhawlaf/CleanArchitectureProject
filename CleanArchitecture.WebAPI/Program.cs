using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Data.Repository;
using CleanArchitecture.FrameworkAndDrivers.Middlewares;
using CleanArchitecture.Infrastructure.Data.Repository;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchitecture.Entities.Users;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FramworkAndDrivers.Data.Repository;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Retrieve JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

if (jwtSettings == null)
{
    throw new InvalidOperationException("JwtSettings section is missing in configuration.");
}

var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
var secretKey = jwtSettings["SecretKey"];

// Validate JWT settings
if (string.IsNullOrEmpty(issuer))
    throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings:Issuer is not configured.");
if (string.IsNullOrEmpty(audience))
    throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings:Audience is not configured.");
if (string.IsNullOrEmpty(secretKey))
    throw new ArgumentNullException(nameof(jwtSettings), "JwtSettings:SecretKey is not configured.");

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register Identity services
builder.Services.AddIdentity<Employee, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;  // Ceci est requis selon l'erreur initiale
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpClient();
// Register application services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TokenService>();

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
     });
    
  
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

// Register Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register specific repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IOrderClientRepository, OrderClientRepository>();
builder.Services.AddScoped<IOrderSupplierRepository, OrderSupplierRepository>();
builder.Services.AddScoped<IDevisRepository, DevisRepository>();

builder.Services.AddScoped<IDeliveryNoteRepository, DeliveryNoteRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProduitSRepository, ProduitSRepository>(); // ServiceRepository doit implémenter IServiceRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository> ();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
// Register domain services
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IOrderClientService, OrderClientService>();
builder.Services.AddScoped<IOrderSupplierService, OrderSupplierService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITaskProjectService, TaskProjectService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ILeaveRequestService,LeaveRequestService>();
builder.Services.AddScoped<INotificationServiceEmail, NotificationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IDeliveryNoteService, DeliveryNoteService>();





// Ajoute la configuration de l'appsettings.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Récupère la configuration
var configuration = builder.Configuration;

// Register other services
builder.Services.AddTransient<IPdfService, PdfService>();
// Ajoutez la configuration pour les paramètres d'email
var emailSettings = builder.Configuration.GetSection("EmailSettings");

builder.Services.AddScoped<IEmailService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var emailSettings = provider.GetRequiredService<IOptions<EmailSettings>>();
    return new EmailService(configuration , emailSettings.Value); // Assurez-vous de passer uniquement IConfiguration
});



// Enregistrement de DinkToPdf
builder.Services.AddSingleton<DinkToPdf.Contracts.ITools, DinkToPdf.PdfTools>();
builder.Services.AddTransient<DinkToPdf.Contracts.IConverter, DinkToPdf.SynchronizedConverter>();

// Ajouter les autres services ici


// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000", "https://localhost:3000", "https://127.0.0.1:3000")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .SetIsOriginAllowed(host => true)
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ValidationMiddleware>();
app.UseDeveloperExceptionPage();

app.MapControllers();

app.Run();
