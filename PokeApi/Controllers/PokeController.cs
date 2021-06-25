using Microsoft.AspNetCore.Mvc;
using PokeApi.AppService.Service;
using PokeApi.Framework;
using System.Threading.Tasks;

namespace PokeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokeController : ControllerBase
    {
        private readonly IPokeService _pokeService;

        public PokeController(IPokeService pokeService)
        {
            _pokeService = pokeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _pokeService.SearchPokeByFilterNameAsync(name);

            return Ok(result);
        }

        [HttpGet]
        [Route("DownloadDetail")]
        public FileContentResult DownloadDetail(int id)
        {
            var result = _pokeService.DownloadDetail(id);

            var httpResult = result.Data.GetFileAsFileContentResult();

            return httpResult;
        }
    }
}
