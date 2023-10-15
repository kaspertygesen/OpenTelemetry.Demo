using Dapper;
using Npgsql;
using OpenTelemetry.Demo.Account.Features.PurchaseCreate.Dto;

namespace OpenTelemetry.Demo.Account.Features.PurchaseCreate;

public class PurchaseCreateHandler
{
    private readonly NpgsqlDataSource _npgsqlDataSource;

    public PurchaseCreateHandler(NpgsqlDataSource npgsqlDataSource)
    {
        _npgsqlDataSource = npgsqlDataSource;
    }
    
    public async Task<long> Handle(PurchaseCreateDto purchase, CancellationToken cancellationToken)
    {
        await using var connection = await _npgsqlDataSource.OpenConnectionAsync(cancellationToken);

        var sqlParams = new
        {
            Owner = owner
        };
        
        var accountId = await connection.ExecuteScalarAsync<long>(@"
INSERT INTO demo.account (owner) 
VALUES (@Owner)
RETURNING id", sqlParams);

        return accountId;
    }
}