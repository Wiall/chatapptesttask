using chatappbackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("https://chatapp-ui.azurewebsites.net") // змінити на твій реальний фронтенд
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Обов'язково для SignalR
    });
});


// Додай Azure SignalR
builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("AzureSignalRConnection");
});

var app = builder.Build();

// Міграція БД
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
// Додай маршрут до хабу
app.MapHub<ChatHub>("/chatHub");

app.Run();