using TODO_API.BAL;
using TODO_API.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IToDoBAL, ToDoBAL>();
builder.Services.AddSingleton<IToDoDAL, ToDoDAL>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
