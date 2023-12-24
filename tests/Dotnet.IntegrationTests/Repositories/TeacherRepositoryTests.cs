using DotNet.TeachersApi.Features;

namespace Dotnet.IntegrationTests.Repositories;

public class TeacherRepositoryTests : IClassFixture<RepositoryFixture>
{
    //private readonly AssetsManagerApiFixture _assetsManagerApiFixture;
    private readonly TeacherRepository _teacherRepository;
    private readonly Fixture _fixture = new();
    //private readonly AddTeacherCreatedClientEvent _teacherCreatedClientEvent;

    public TeacherRepositoryTests()
    {
        //    _assetsManagerApiFixture = assetsManagerApiFixture;
        _teacherRepository = new TeacherRepository(PostgresqlFixture.Context!);
        //    _teacherCreatedClientEvent = new AddTeacherCreatedClientEvent(BrokerFixture.Broker!);
    }

    [Fact]
    public void Given_valid_teacher_request_should_insert_correctly()
    {
        //Arrange
        var orders = TeacherFactory.CreateIssuance(1);

        // Act
        _teacherRepository.InsertMany(orders);

        // Assert
        var ordersCount = PostgresqlFixture.Context!.Teachers.Count;
        Assert.Equal(1, ordersCount);
        //ordersCount.Should().Be(ordersToAdd.Count);
        //copyResult.IsSuccess.Should().BeTrue();
    }

}