using ORAA.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container using your service extensions
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS middleware (must be before authentication/authorization)
app.UseCors();

// Add authentication and authorization middleware in the correct order
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();