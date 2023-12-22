namespace Dotnet.IntegrationTests
{
    internal class OrderCancellationReasonRequest
    {
        public OrderCancellationReasonEnum Type { get; set; }
        public string Description { get; set; }
    }
}