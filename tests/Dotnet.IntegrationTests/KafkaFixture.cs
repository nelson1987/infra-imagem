using DotNet.TeachersApi.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;

namespace Dotnet.IntegrationTests;

public static class KafkaFixture
{
    private static readonly IEventClient? EventClient;
    public static void ConfigureKafkaServices(IServiceCollection services)
        => services
            //.RemoveAll<KafkaHealthCheck>()
            .RemoveAll<IEventClient>()
            //.RemoveAll<IEventClientConsumers>()
            //.RemoveAll<IEventClientProducer>()
            .AddSingleton(_ => EventClient);
    //.AddSingleton<IEventClientConsumers>(_ => EventClientConsumers)
    //.AddSingleton<IEventClientProducer>(_ => EventClientProducer);
    public static Task Produce<T>(T data)
    {
        using var cancellationToken = ExpiringCancellationToken();
        AddTeacherCreatedClientEvent? evento = data as AddTeacherCreatedClientEvent;
        return EventClient.Produce(
                evento,
                cancellationToken.Token);
    }
    public static T Consume<T>(string topic, int timeout = 150)
    {
        try
        {
            using var cancellationToken = ExpiringCancellationToken(timeout);
            return EventClient.Consume(topic, JsonSerializer.Deserialize<T>, cancellationToken.Token);
        }
        catch { return default!; }
    }
    private static CancellationTokenSource ExpiringCancellationToken(int msTimeout = 150)
    {
        var timeout = TimeSpan.FromMilliseconds(msTimeout);
        return new CancellationTokenSource(timeout);
    }
}
public interface IEventClient
{
    T Consume<T>(string topicName, Func<Stream, JsonSerializerOptions?, T?> deserialize, CancellationToken token = default);
    Task Produce(AddTeacherCreatedClientEvent? addTeacherCreatedClientEvent, CancellationToken token);
}
public static class PostgresqlFixture
{
    public static DatabaseContext? Context { get; private set; }

    static PostgresqlFixture() { Context = new DatabaseContext(); }
}
public static class BrokerFixture
{
    public static Broker? Broker { get; private set; }

    static BrokerFixture() { Broker = new Broker(); }
}