using DotNet.TeachersApi.Features;

namespace Dotnet.IntegrationTests;

public static class TeacherFactory
{
    private static readonly Fixture _fixture = new Fixture();
    public static IEnumerable<Teacher> CreateIssuance(int amount = 2)
    {
        var orderId = Guid.NewGuid();

        return Enumerable.Range(0, amount)
                  .Select(_ => CreateIssuance(orderId));
    }
    public static Teacher CreateIssuance(Guid? orderId = null)
    {
        //var asset = CreateAsset();
        //orderId ??= Guid.NewGuid();

        //var cashflows = _fixture.Build<OrderCashflow>()
        //        .With(x => x.OrderId, orderId)
        //        .Without(x => x.Order)
        //        .CreateMany()
        //        .ToList();

        return _fixture
                .Build<Teacher>()
                .With(x => x.Id, orderId)
                //.With(x => x.InternalId, orderId)
                //.With(x => x.AssetId, asset.Id)
                //.With(x => x.Type, OrderType.Issue)
                //.With(o => o.Asset, asset)
                //.With(c => c.Cashflows, cashflows)
                .Create();
    }
}
