using LolStatsAPI.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Marveldle.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetValue<string>(Environment.OSVersion.Platform == PlatformID.Win32NT ? "LocalConnectionString" : "DockerConnectionString");
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient
);

builder.Services.AddHostedService<DataLoadingService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
