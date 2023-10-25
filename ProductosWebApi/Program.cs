using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using ProductosWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var server = "sqldata";
var port = "1433";
var user = "sa";
var password = "yourStrong(!)Password";
var database = "Productos";

builder.Services.AddDbContext<ProductoDbContext>(options =>
{
    options.UseSqlServer($"Server={server},{port};Database={database};User ID={user};Password={password};Encrypt=False");

});

builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
builder.Services.AddSingleton<IMessageSuscriber, MessageSuscriber>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductoDbContext>();
    context.Database.Migrate();
}

//app.Services.GetRequiredService<IMessageConsumer>().StartConsumer();
app.Services.GetRequiredService<IMessageSuscriber>().Suscribe();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
