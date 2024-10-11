using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using WebApiCurso.Context;
using WebApiCurso.DTOs.Mappings;
using WebApiCurso.Repositories;
using WebApiCurso.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler
    = ReferenceHandler.IgnoreCycles);
//CORS

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://www.apirequest.io")
                .WithMethods("GET, POST") // restrigindo metodos HTTP
                .AllowCredentials() //Permite o envio de credenciais 
                .AllowAnyHeader(); // permite qualquer cabecalho
        })
);

builder.Services.AddRateLimiter(o =>
{
    o.AddFixedWindowLimiter("FixedWindow", options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(5);
        options.QueueLimit = 0;
    });
    o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "APICatalogo",
        Description = "Catalogo de Produtos e categorias",
        Contact = new OpenApiContact
        {
            Name = "Joao Victor Martinho",
            Email = "Joao.victor.martinho2@gmail.com"
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Adicione a extensão .xml
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

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
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.MapControllers();
app.Run();