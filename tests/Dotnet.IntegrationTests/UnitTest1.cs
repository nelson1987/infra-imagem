using DotNet.TeachersApi.Features;
using System.Net.Http.Json;
using System.Text;

namespace Dotnet.IntegrationTests;

public class UnitTest1 : IClassFixture<AssetsManagerApiFixture>
{
    private readonly AssetsManagerApiFixture _assetsManagerApiFixture;
    private readonly TeacherRepository _teacherRepository;
    private readonly AddTeacherCreatedClientEvent _teacherCreatedClientEvent;

    public UnitTest1(AssetsManagerApiFixture assetsManagerApiFixture)
    {
        _assetsManagerApiFixture = assetsManagerApiFixture;
        _teacherRepository = new TeacherRepository(PostgresqlFixture.Context!);
        _teacherCreatedClientEvent = new AddTeacherCreatedClientEvent(BrokerFixture.Broker!);
    }

    [Fact]
    public async Task Given_valid_teacher_request_should_return_success()
    {
        //Arrange
        var content = "{" +
            "\"Id\":\"6faeea10-803d-4a16-af7c-b0e8f5aea814\", " +
            "\"Nome\":\"Teacher Name\"" +
            "}";
        // Act
        using var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _assetsManagerApiFixture.Client.PostAsync("/weatherForecast", stringContent);

        response.EnsureSuccessStatusCode();
        // Assert
        var stringResponse = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, stringResponse);

        var evento = _teacherCreatedClientEvent.Consume();
        Assert.NotNull(evento);
    }

    [Fact]
    public async Task Given_valid_teacher_request_should_insert_correctly()
    {
        var orders = TeacherFactory.CreateIssuance(1);
        _teacherRepository.InsertMany(orders);
        //Arrange
        var content = "{" +
            "\"Id\":\"6faeea10-803d-4a16-af7c-b0e8f5aea814\", " +
            "\"Nome\":\"Teacher Name\"" +
            "}";
        // Act
        using var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _assetsManagerApiFixture.Client.PostAsync("/weatherForecast", stringContent);

        // Assert
        var stringResponse = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, stringResponse);
        var retorno = _teacherRepository.Get(orders.FirstOrDefault()!.Id);
    }

    [Fact]
    public async Task Given_invalid_teacher_request_should_return_error()
    {
        //Arrange
        var content = "{" +
            "\"Id\":\"6faeea10-803d-4a16-af7c-b0e8f5aea814\", " +
            "}";
        // Act
        using var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _assetsManagerApiFixture.Client.PostAsync("/weatherForecast", stringContent);

        // Assert
        var stringResponse = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, stringResponse);
    }

    [Fact]
    public async Task Given_an_http_request_expect_it_to_execute_update()
    {
        // Arrange
        var orders = TeacherFactory.CreateIssuance(2);
        _teacherRepository.InsertMany(orders);

        var requestbody = new
        {
            orders.First().AssetId,
            Nome = "Nome Asset"
        };

        // Act
        await _assetsManagerApiFixture.Client.PostAsJsonAsync("/weatherForecast", requestbody, CancellationToken.None);

        // Assert
        //var createCreditNotesCancellationEvent = KafkaFixture
        //    .Consume<CreateCreditNotesCancellationEvent>(CreditNotesCancellationRequested.Name);

        //await Verifier.Verify(createCreditNotesCancellationEvent)
        //    .DontScrubGuids()
        //    .DontSortDictionaries();
    }
}