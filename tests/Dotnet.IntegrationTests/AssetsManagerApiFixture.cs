using System.Net.Http.Headers;

namespace Dotnet.IntegrationTests;

public class AssetsManagerApiFixture
{
#pragma warning disable IDE1006 // Estilos de Nomenclatura
    private static readonly AssetsManagerApi _server;
    private static readonly HttpClient _client;
    //public static readonly DatabaseContext _contexto;
#pragma warning restore IDE1006 // Estilos de Nomenclatura

#pragma warning disable CA1822 // Marcar membros como estáticos
    public AssetsManagerApi Server => _server;
    public HttpClient Client => _client;
    //public DatabaseContext Contexto => _contexto;
#pragma warning restore CA1822 // Marcar membros como estáticos

    static AssetsManagerApiFixture()
    {
        _server = new();
        _client = _server.CreateDefaultClient();
        //_contexto = _server.Services.GetRequiredService<DatabaseContext>();
    }

    public AssetsManagerApiFixture()
    {
        _client.DefaultRequestHeaders.Clear();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");
    }
}
public class DatabaseContextFixture { }
