using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Kafka;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICurityCredentialService, CurityCredentialService>();
builder.Services.AddScoped<CreateCurityCredentialUseCase>();
builder.Services.AddHostedService<CurityKafkaConsumer>();

var app = builder.Build();
app.Run();