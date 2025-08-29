using MinhaLojaExpress.API.Infra;
using MinhaLojaExpress.Aplicacao;
using MinhaLojaExpress.Infra;
using MinhaLojaExpress.Infra.Contexto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddInfra(configuration);
builder.Services.AddAplicacao();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var initialiser = services.GetRequiredService<InicializadorBancoDeDados>();
    await initialiser.InicializarAsync();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });
app.UseAuthorization();
app.MapControllers();

app.Run();
