using System.Collections.Generic;

namespace PokeApi.AppService.Dto
{
    public class PokemonsResult
    {
        public int count { get; set; }
        public List<Pokemons> results { get; set; } = new List<Pokemons>();
    }

    public class Pokemons
    {
        public string name { get; set; }
    }
}
