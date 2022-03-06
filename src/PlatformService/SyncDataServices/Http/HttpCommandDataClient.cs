using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommandAsync(PlatformToRead platform)
    {
        StringContent httpContent = new(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json");
        HttpResponseMessage response = await _client.PostAsync(_configuration.CommandsService(), httpContent);
        Console.WriteLine(response.IsSuccessStatusCode
            ? "==> Sync POST to CommandsService was OK!"
            : "==> Sync POST to CommandsService was NOT OK!");
    }
}
