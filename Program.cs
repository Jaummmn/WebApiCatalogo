using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiCurso.Context;
using WebApiCurso.DTOs.Mappings;
using WebApiCurso.Repositories;
using WebApiCurso.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler
    = ReferenceHandler.IgnoreCycles);
var origensComAcessoPermitido = "_origensComAcessoPermitido";
builder.Services.AddCors(options =>
    options.AddPolicy(name: origensComAcessoPermitido,
        policy =>
        {
            policy.WithOrigins("https://www.apirequest.io");
        })
    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutosRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(ProdutoDTOMappingProfile));
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();