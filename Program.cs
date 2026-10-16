using BD_TRANSACCIONES.BLL;
using BD_TRANSACCIONES.DAL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

// INYECCION DE MONEDA
builder.Services.AddScoped<MonedaDAL>();
builder.Services.AddScoped<MonedaBLL>();

// INYECCION DE CUENTA
builder.Services.AddScoped<CuentaDAL>();
builder.Services.AddScoped<CuentaBLL>();

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

app.Run();
