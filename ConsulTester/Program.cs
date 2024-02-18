using Api1;
using Coordinator.HealthChecking;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddHealthChecks()
                    .AddCheck<MainHealthCheck>("Sample");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterWithConsulServiceDiscovery(builder.Configuration.GetSection("ConsulServiceDiscovery"));

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapHealthChecks("/healthz");
app.UseSampleEndpoints(builder.Environment);

app.Run();