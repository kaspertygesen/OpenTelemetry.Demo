using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace OpenTelemetry.Demo.Account.Features.AccountCreate;

internal static class AccountCreateEndpoint
{
    internal static WebApplication RegisterAccountCreateEndpoint(this WebApplication app)
    {
        var groupTag = new OpenApiTag { Name = "Account", Description = "Endpoints for managing accounts" };
        var tags = new List<OpenApiTag> { groupTag };

        app.MapPost("v1/account", Ok<Guid> ([FromBody] Models.Account account, CancellationToken cancellationToken) =>
            {
                return TypedResults.Ok(account.AccountId);
            })
            .Accepts<Models.Account>("application/json")
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