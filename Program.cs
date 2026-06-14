    using Microsoft.EntityFrameworkCore;
    using PMS.Api.Data;
    using PMS.Api.Interfaces;
    using PMS.Api.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using PMS.Api.Middleware;
    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddControllers();

    builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );

    builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IProjectService, ProjectService>();
    builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType
                                    .SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});


    builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!
                    )
                )
        };
});

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<ExceptionMiddleware>();

    // Enable Auth Middleware
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();

