using RestWithASPNET10Erudio.Data.DTO.V1;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RestWithASPNET10Erudio.Tests.IntegrationTests.Tools
{
    public static class AuthHelper
    {
        private const string SeedUsername = "erudio";
        private const string SeedPassword = "admin123";

        public static void Authenticate(HttpClient httpClient)
        {
            var credentials = new UserDTO
            {
                Username = SeedUsername,
                Password = SeedPassword
            };

            var response = httpClient
                .PostAsJsonAsync("api/auth/signin", credentials)
                .GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();

            var token = response.Content
                .ReadFromJsonAsync<TokenDTO>()
                .GetAwaiter().GetResult();

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer", token!.AccessToken);
        }
    }
}
