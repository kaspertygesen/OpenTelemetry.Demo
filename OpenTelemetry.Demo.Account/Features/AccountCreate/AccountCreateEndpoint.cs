using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Demo.Account.Features.AccountCreate.Dto;

namespace OpenTelemetry.Demo.Account.Features.AccountCreate;

internal static class AccountCreateEndpoint
{
    internal static IServiceCollection RegisterAccountCreateDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<AccountCreateHandler>();
    }
    
    internal static WebApplication RegisterAccountCreateEndpoint(this WebApplication app)
    {
        var groupTag = new OpenApiTag { Name = "Account", Description = "Endpoints for managing accounts" };
        var tags = new List<OpenApiTag> { groupTag };

        app.MapPost("v1/account", async Task<Ok<long>> ([FromBody] AccountCreateDto dto, AccountCreateHandler handler, CancellationToken cancellationToken) =>
            {
                var accountId = await handler.Handle(dto.Owner, cancellationToken);
                
                return TypedResults.Ok(accountId);
            })
            .Accepts<AccountCreateDto>("application/json")
            .WithOpenApi(x =>
            {
                x.Tags = tags;
                x.Summary = "Creates a new account and returns the id";
                x.Description = "Creates a new account and returns the id";

                return x;
            });
        return app;
    }
}