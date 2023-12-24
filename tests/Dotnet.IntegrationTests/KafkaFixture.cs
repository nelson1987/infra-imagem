using DotNet.TeachersApi.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;

namespace Dotnet.IntegrationTests;

public static class KafkaFixture
{
#pragma warning disable CS0649 // Campo "KafkaFixture.EventClient" nunca é atribuído e sempre terá seu valor padrão null
    private static readonly IEventClient? EventClient;
#pragma warning restore CS0649 // Campo "KafkaFixture.EventClient" nunca é atribuído e sempre terá seu valor padrão null
    public static void ConfigureKafkaServices(IServiceCollection services)
#pragma warning disable CS8634 // O tipo não pode ser usado como parâmetro de tipo no tipo ou método genérico. A anulabilidade do argumento de tipo não corresponde à restrição 'class'.
#pragma warning disable CS8621 // A nulidade de tipos de referência no tipo de retorno não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
        => services
            //.RemoveAll<KafkaHealthCheck>()
            .RemoveAll<IEventClient>()
            //.RemoveAll<IEventClientConsumers>()
            //.RemoveAll<IEventClientProducer>()
            .AddSingleton(_ => EventClient);
#pragma warning restore CS8621 // A nulidade de tipos de referência no tipo de retorno não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
#pragma warning restore CS8634 // O tipo não pode ser usado como parâmetro de tipo no tipo ou método genérico. A anulabilidade do argumento de tipo não corresponde à restrição 'class'.
    //.AddSingleton<IEventClientConsumers>(_ => EventClientConsumers)
    //.AddSingleton<IEventClientProducer>(_ => EventClientProducer);
    public static Task Produce<T>(T data)
    {
        using var cancellationToken = ExpiringCancellationToken();
        AddTeacherCreatedClientEvent? evento = data as AddTeacherCreatedClientEvent;
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
        return EventClient.Produce(
                evento,
                cancellationToken.Token);
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
    }
    public static T Consume<T>(string topic, int timeout = 150)
    {
        try
        {
            using var cancellationToken = ExpiringCancellationToken(timeout);
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            return EventClient.Consume(topic, JsonSerializer.Deserialize<T>, cancellationToken.Token);
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
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