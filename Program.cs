using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
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
        policy => { policy.WithOrigins("https://www.apirequest.io"); })
);
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader(),
        new UrlSegmentApiVersionReader());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
    c.SwaggerDoc("v1",new OpenApiInfo()
    {
     Version   = "v1",
     Title = "APICatalogo",
     Description = "Catalogo de Produtos e categorias",
        Contact = new OpenApiContact
        {
            Name = "Joao Victor Martinho",
            Email = "Joao.victor.martinho2@gmail.com"
        }
    })
    );
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