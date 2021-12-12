using Microsoft.AspNetCore.DataProtection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var keysFolder = Path.Combine(builder.Environment.ContentRootPath, "Keys");

builder.Services.AddDataProtection()
.PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
 .SetApplicationName("Apollo")
 .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

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

app.Run();