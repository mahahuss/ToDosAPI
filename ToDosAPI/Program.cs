using ToDosAPI;
using ToDosAPI.Models.Dapper;
using ToDosAPI.Models.TaskClasses;
using ToDosAPI.Models.UserClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserServices, UserServices>();
builder.Services.AddSingleton<DapperDBContext>();
builder.Services.AddSingleton<ITaskServices, TaskServices>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class Scoped
{

    public Scoped(Singleton singleton)
    {

    }
};
public class Singleton { };