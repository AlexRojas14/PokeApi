namespace PokeApi.AppService.Dto
{
    public class Abilities
    {
        public Ability ability { get; set; }
        public bool is_hidden { get; set; }
    }

    public class Ability
    {
        public string name { get; set; }
    }
}
