using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using ToDosAPI;
using ToDosAPI.Data;
using ToDosAPI.Services;

const string corsOrigins = "todos_origin";

var builder = WebApplication.CreateBuilder(args);
var retainedFileTimeLimit = builder.Configuration.GetSection("RetainedFileDaysLimit").Get<uint>();

if (retainedFileTimeLimit == 0) throw new Exception("set the retainedFileTimeLimit value in app-settings");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["LogsPath"]!, rollingInterval: RollingInterval.Minute,
        retainedFileTimeLimit: TimeSpan.FromDays(retainedFileTimeLimit))
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddCors(p =>
{
    p.AddPolicy(corsOrigins, c => { c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserTaskRepository>();
builder.Services.AddSingleton<DapperDbContext>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<PasswordHasherService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<FileService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();