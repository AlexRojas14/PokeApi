using PokeApi.AppService.Dto;
using System.Text;

namespace PokeApi.AppService.Framework
{
    public static class PokeDtoExtensions
    {
        public static StringBuilder ToFixedRow(this PokeDto pokeDetail)
        {
            var stringBuild = new StringBuilder();

            if (pokeDetail == null)
                return stringBuild;

            var fixedPokeHeader = GetStringPadR("Id", 7);
            fixedPokeHeader += GetStringPadR("Nombre", 20);
            fixedPokeHeader += GetStringPadR("Experiencia", 20);
            fixedPokeHeader += GetStringPadR("Altura", 13);
            fixedPokeHeader += GetStringPadR("Predeterminado", 20);
            fixedPokeHeader += GetStringPadR("Orden", 10);
            fixedPokeHeader += GetStringPadR("Peso", 10);

            stringBuild.AppendLine(fixedPokeHeader);

            var fixedPokeRow = GetStringFromInt(pokeDetail.id, 7);
            fixedPokeRow += GetStringPadR(pokeDetail.name, 20);
            fixedPokeRow += GetStringFromInt(pokeDetail.base_experience, 20);
            fixedPokeRow += GetStringFromInt(pokeDetail.height, 13);
            fixedPokeRow += GetStringPadR(pokeDetail.is_default ? "true" : "false", 20);
            fixedPokeRow += GetStringFromInt(pokeDetail.order, 10);
            fixedPokeRow += GetStringFromInt(pokeDetail.weight, 10);

            stringBuild.AppendLine(fixedPokeRow);

            stringBuild.AppendLine("\n--------------Habilidades--------------");

            var fixedAbilityHeader = GetStringPadR("Nombre", 30);
            fixedAbilityHeader += GetStringPadR("Es Oculta", 10);

            stringBuild.AppendLine(fixedAbilityHeader);

            foreach (var ability in pokeDetail.abilities)
            {
                var fixedAbilityRow = GetStringPadR(ability.ability.name, 30);
                fixedAbilityRow += GetStringPadR(ability.is_hidden ? "true" : "false", 10);

                stringBuild.AppendLine(fixedAbilityRow);
            }

            stringBuild.AppendLine("\n----------------------------------------------Imagenes----------------------------------------------");

            var sprites = pokeDetail.sprites;

            stringBuild.AppendLine(GetStringPadR(sprites.back_default, 100));
            stringBuild.AppendLine(GetStringPadR(sprites.back_shiny, 100));
            stringBuild.AppendLine(GetStringPadR(sprites.front_default, 100));
            stringBuild.AppendLine(GetStringPadR(sprites.front_shiny, 100));

            return stringBuild;
        }

        private static string GetStringPadR(string value, int length, char pad = ' ')
        {
            return value == null ? "".PadRight(length, ' ') : value.PadRight(length, pad).Substring(0, length);

        }

        private static string GetStringFromInt(int value, int length, char pad = ' ')
        {
            return GetStringPadR(value.ToString(), length, pad);
        }
    }
}
