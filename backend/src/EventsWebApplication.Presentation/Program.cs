using EventsWebApplication.Application;
using EventsWebApplication.Infrastructure;
using EventsWebApplication.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();
await app.StartApplication();