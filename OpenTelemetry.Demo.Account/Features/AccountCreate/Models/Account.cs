namespace OpenTelemetry.Demo.Account.Features.AccountCreate.Models;

public record Account(AccountOwner Owner, List<AccountTransaction> Transactions)
{
    public Guid AccountId { get; } = Guid.NewGuid();
}