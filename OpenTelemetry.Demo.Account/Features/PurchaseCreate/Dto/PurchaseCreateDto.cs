namespace OpenTelemetry.Demo.Account.Features.PurchaseCreate.Dto;

public record PurchaseCreateDto(long AccountId, string Product, decimal Amount, long SponsorAccountId, decimal SponsorShip);