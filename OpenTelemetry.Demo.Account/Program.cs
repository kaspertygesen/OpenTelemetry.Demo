using OpenTelemetry.Demo.Account.Features.AccountCreate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNpgsqlDataSource(builder.Configuration["ConnectionStrings:Postgres"]!);

builder.Services.RegisterAccountCreateDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.RegisterAccountCreateEndpoint();

app.Run();