using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Kafka;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICurityCredentialService, CurityCredentialService>();
builder.Services.AddScoped<CreateCurityCredentialUseCase>();
builder.Services.AddScoped<CredentialRepository>();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=credentials.db"));

builder.Services.AddHostedService<CurityKafkaConsumer>();

var app = builder.Build();
app.Run();