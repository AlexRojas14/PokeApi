using Microsoft.AspNetCore.Mvc;
using PokeApi.AppService.Service;
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
            var result = await _pokeService.GetPokeByNameAsync(name);

            return Ok(result);
        }
    }
}
