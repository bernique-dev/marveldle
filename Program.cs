using LolStatsAPI.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Marveldle.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
     });
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
builder.Services.AddHostedService<DailyCharacterPickerService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);


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

app.UseStaticFiles();

app.MapControllers();

app.Run();
