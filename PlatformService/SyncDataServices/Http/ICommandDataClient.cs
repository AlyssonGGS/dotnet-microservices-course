using PlatformService.Dtos;

namespace PlatformService.SyncDataService;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto plat);
}