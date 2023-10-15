using Dapper;
using Npgsql;

namespace OpenTelemetry.Demo.Account.Features.AccountCreate;

public class AccountCreateHandler
{
    private readonly NpgsqlDataSource _npgsqlDataSource;

    public AccountCreateHandler(NpgsqlDataSource npgsqlDataSource)
    {
        _npgsqlDataSource = npgsqlDataSource;
    }
    
    public async Task<long> Handle(string owner, CancellationToken cancellationToken)
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