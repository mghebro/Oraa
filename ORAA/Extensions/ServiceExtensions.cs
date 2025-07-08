
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ORAA.Models;
using ORAA.Services.Implementations;
using ORAA.Services.Interfaces;
using ORAA.Data;

namespace ORAA.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBasicServices();
        services.AddDatabaseServices(configuration);
        services.AddIdentityServices();
        services.AddBusinessServices();
        services.AddAuthenticationServices(configuration);
        services.AddAuthorizationServices();
        services.AddOtherServices();
        return services;
    }

    private static IServiceCollection AddBasicServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    private static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    


        // Apple payment service
      //       services.AddScoped<IApplePaymentService, ApplePaymentService>();
       //      services.AddHttpClient<IApplePaymentService, ApplePaymentService>();

        return services;
    }

    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Service
        services.AddScoped<IJWTService, JWTService>();

        // Get JWT configuration
        var jwtKey = configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT:Key not found in configuration");
        var jwtIssuer = configuration["JWT:Issuer"] ?? throw new InvalidOperationException("JWT:Issuer not found in configuration");
        var jwtAudience = configuration["JWT:Audience"] ?? throw new InvalidOperationException("JWT:Audience not found in configuration");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddCookie()
            .AddGoogle(options =>
            {
                var clientId = configuration["Authentication:Google:ClientId"] ??
                    throw new ArgumentNullException("Authentication:Google:ClientId");
                var clientSecret = configuration["Authentication:Google:ClientSecret"] ??
                    throw new ArgumentNullException("Authentication:Google:ClientSecret");

                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        return services;
    }

    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            options.AddPolicy("HostOnly", policy => policy.RequireRole("Host"));
            options.AddPolicy("Universal", policy => policy.RequireRole("Owner, Admin"));
        });

        return services;
    }

    private static IServiceCollection AddOtherServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        return services;
    }
}