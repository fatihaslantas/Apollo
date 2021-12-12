using Apollo.Api.Services;
using Apollo.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IEncryptionService, EncryptionService>()
              .ConfigureHttpClient((serviceProvider, httpClient) =>
              {
                  string baseUrl = builder.Configuration.GetSection("EncryptionService").GetSection("BaseUrl").Value;

                  httpClient.BaseAddress = new Uri(baseUrl);
                  httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
              });



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
