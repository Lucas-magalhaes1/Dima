using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Dima.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();


if (builder.Environment.IsDevelopment()) 
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build(); 

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapAndpoints();

app.Run();
