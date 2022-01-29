using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataService.Http;
public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _config = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync($"{_config["CommandService"]}/api/command/platforms/", httpContent);

        if(!response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to command service was not ok");
            return;
        }
        
        Console.WriteLine("--> Sync POST to command service was ok");
    }
}