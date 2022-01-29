using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _dataClient;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient dataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _dataClient = dataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms() 
        {
            Console.WriteLine("--> Getting platforms...");

            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
        }

        [HttpGet("{id}", Name= "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);
            if(platformItem == null) return NotFound();
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreate)
        {
            var platform = _mapper.Map<Platform>(platformCreate);
            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var readDto = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _dataClient.SendPlatformToCommand(readDto);
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Could not send synchronously: {e.Message}");
            }
            

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = readDto.Id}, readDto);
        }
    }
}