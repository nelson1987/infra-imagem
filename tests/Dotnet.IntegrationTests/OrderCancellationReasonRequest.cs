namespace Dotnet.IntegrationTests;

internal class OrderCancellationReasonRequest
{
    public OrderCancellationReasonEnum Type { get; set; }
    public required string Description { get; set; }
}