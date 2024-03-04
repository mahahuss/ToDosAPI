﻿using System.Text;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDosAPI.Data;
using ToDosAPI.Migrations;
using ToDosAPI.Services;

namespace ToDosAPI.Extensions;

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