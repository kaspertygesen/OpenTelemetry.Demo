using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Demo.Account.Features.AccountCreate;
using OpenTelemetry.Demo.Account.Features.AccountCreate.Dto;
using OpenTelemetry.Demo.Account.Features.PurchaseCreate.Dto;

namespace OpenTelemetry.Demo.Account.Features.PurchaseCreate;

internal static class PurchaseCreateEndpoint
{
    internal static IServiceCollection RegisterPurchaseCreateDependencies(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<PurchaseCreateHandler>();
    }
    
    internal static WebApplication RegisterPurchaseCreateEndpoint(this WebApplication app)
    {
        var groupTag = new OpenApiTag { Name = "Purchases", Description = "Endpoints for managing purchases" };
        var tags = new List<OpenApiTag> { groupTag };

        app.MapPost("v1/purchase", async Task<Ok<long>> ([FromBody] PurchaseCreateDto dto, PurchaseCreateHandler handler, CancellationToken cancellationToken) =>
            {
                var accountId = await handler.Handle(dto.Owner, cancellationToken);
                
                return TypedResults.Ok(accountId);
            })
            .Accepts<AccountCreateDto>("application/json")
            .WithOpenApi(x =>
            {
                x.Tags = tags;
                x.Summary = "Create a new purchase";
                x.Description = "Creates a new purchase and register sponsorship";

                return x;
            });
        return app;
    }
}