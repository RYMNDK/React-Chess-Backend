using Chess_Backend.Domains;
using Microsoft.EntityFrameworkCore;
// using ReactChess.Server.Services;
// using ReactChess.Server.Domain;
// using ReactChess.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR(option =>
{
    option.KeepAliveInterval = TimeSpan.FromSeconds(15);
    option.ClientTimeoutInterval = TimeSpan.FromSeconds(35);
});
builder.Services.AddControllers();

// todo: figure out CORS settings
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("CorsPolicy", request =>
//     {
//         request.WithOrigins("https://localhost:3000", "https://localhost:3001")
//             .AllowAnyHeader()
//             .AllowAnyMethod()
//             .AllowCredentials();
//     });
// });

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    // dbContext.Database.EnsureDeleted();
    await dbContext.Database.MigrateAsync();

}

app.Run();