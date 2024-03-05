using FluentMigrator.Runner;
using Serilog;
using ToDosAPI;
using ToDosAPI.Extensions;

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
builder.AddAppServices();
builder.AddTodosCors(corsOrigins);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddAppAuth();
builder.AddDbMigrations();
builder.Services.AddHttpClient();

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
using var scope = app.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();
app.Run();