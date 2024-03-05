using System.Text;
using Api.Data;
using Api.Migrations;
using Api.Services;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class AppServicesExtensions
{
    public static void AddAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton<UserTaskRepository>();
        builder.Services.AddSingleton<DapperDbContext>();
        builder.Services.AddSingleton<TaskService>();
        builder.Services.AddSingleton<PasswordHasherService>();
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddSingleton<FileService>();
        builder.Services.AddHttpClient<JsonPlaceholderService>(c =>
        {
            c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        });
        builder.Services.AddSingleton<PostRepository>();
        builder.Services.AddSingleton<PostService>();
    }

    public static void AddTodosCors(this WebApplicationBuilder builder, string corsName)
    {
        builder.Services.AddCors(p =>
        {
            p.AddPolicy(corsName, c =>
            {
                c.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public static void AddAppAuth(this WebApplicationBuilder builder)
    {
//Jwt configuration starts here
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtKey = builder.Configuration["Jwt:Key"];

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
                };
            });
    }

    public static void AddDbMigrations(this WebApplicationBuilder builder)
    {
        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(builder.Configuration.GetConnectionString("TodosDb"))
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                .WithVersionTable(new MigrationTableMetadata()))
            .AddLogging(lb => lb.AddFluentMigratorConsole());
    }
}