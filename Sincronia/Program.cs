using Sincronia.Services;

var builder = WebApplication.CreateBuilder(args);

// Asegúrate de tener estas líneas para inyectar tus nuevos servicios

builder.Services.AddHttpClient();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<PlannerAiService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configuración de CORS para permitir que el Front consuma la API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();