using Microsoft.AspNetCore.Mvc;
using PokeApi.AppService.Framework;
using PokeApi.AppService.Service;
using System.Net.Http;
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

        public async Task<IActionResult> Get(string name)
        {
            var result = await _pokeService.GetPokeByNameAsync(name);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public HttpResponseMessage DownloadDetail(int id)
        {
            var result = _pokeService.DownloadDetail(id);

            var httpResult = result.Data.GetFileAsHttpResponseMessage();

            return httpResult;
        }
    }
}
