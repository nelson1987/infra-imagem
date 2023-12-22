using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Dotnet.IntegrationTests
{
    public class AssetsManagerApi : WebApplicationFactory<Program>
    {
        static AssetsManagerApi()
            => Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Test");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
            => builder.UseEnvironment("Test")
                      .ConfigureTestServices(services =>
                      {
                          services.AddAuthentication(defaultScheme: "TestScheme")
                                  .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

                          KafkaFixture.ConfigureKafkaServices(services);
                      });

        private class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
                ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
            { }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            {
                var claims = new[] {
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim("preferred_username", "user@stone.com.br")
            };
                var identity = new ClaimsIdentity(claims, "Test");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "TestScheme");

                var result = AuthenticateResult.Success(ticket);

                return Task.FromResult(result);
            }
        }
    }
}
