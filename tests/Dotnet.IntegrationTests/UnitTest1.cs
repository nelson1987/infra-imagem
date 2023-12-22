using System.Net.Http.Json;
using System.Text;

namespace Dotnet.IntegrationTests
{
    public class UnitTest1 : IClassFixture<AssetsManagerApiFixture>
    {
        private readonly AssetsManagerApiFixture _assetsManagerApiFixture;
        private readonly TeacherRepository _teacherRepository;

        public UnitTest1(AssetsManagerApiFixture assetsManagerApiFixture)
        {
            _assetsManagerApiFixture = assetsManagerApiFixture;
            _teacherRepository = new TeacherRepository();
                //PostgresqlFixture.Context,
                //PostgresqlFixture.ConnectionFactory,
                //new PostgresCompiler());
        }

        [Fact]
        public async Task Given_valid_teacher_request_should_return_success()
        {
            //Arrange
            var content = "{}";
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

            //await _teacherRepository.InsertMany(orders);

            var requestbody = new
            {
                orders.First().AssetId,
                AffectedOrders = orders.Select(x => x.Id).ToArray(),
                Reason = new OrderCancellationReasonRequest
                {
                    Type = OrderCancellationReasonEnum.Fraud,
                    Description = "Fraud description"
                }
            };

            // Act
            await _assetsManagerApiFixture.Client.PostAsJsonAsync("/weatherForecast", requestbody, CancellationToken.None);

            // Assert
            var createCreditNotesCancellationEvent = KafkaFixture
                .Consume<CreateCreditNotesCancellationEvent>(CreditNotesCancellationRequested.Name);

            //await Verifier.Verify(createCreditNotesCancellationEvent)
            //    .DontScrubGuids()
            //    .DontSortDictionaries();
        }
    }
}