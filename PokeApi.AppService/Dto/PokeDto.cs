using System.Collections.Generic;

namespace PokeApi.AppService.Dto
{
    public class PokeDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int base_experience { get; set; }
        public int height { get; set; }
        public bool is_default { get; set; }
        public int order { get; set; }
        public int weight { get; set; }

        public List<Abilities> abilities { get; set; } = new List<Abilities>();
        public Sprites sprites { get; set; } = new Sprites();
    }
}
