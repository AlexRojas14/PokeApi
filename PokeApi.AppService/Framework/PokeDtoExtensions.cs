using PokeApi.AppService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApi.AppService.Framework
{
    public static class PokeDtoExtensions
    {
        public static StringBuilder ToFixedRow(this PokeDto pokeDetail)
        {
            var stringBuild = new StringBuilder();
            var fixedRow = "";

            string[] headers =
            {
                "Id",
                "Nombre",
                "Experiencia de Base",
                "Altura",
                "Es Predeterminado",
                "Orden",
                "Peso"
            };

            foreach (var header in headers)
            {
                fixedRow += GetStringPadR(header, 10);
            }

            stringBuild.AppendLine(fixedRow);

            fixedRow += GetStringFromInt(pokeDetail.id, 1);
            fixedRow += GetStringPadR(pokeDetail.name, 10);
            fixedRow += GetStringFromInt(pokeDetail.base_experience, 40);
            fixedRow += GetStringFromInt(pokeDetail.height, 40);
            fixedRow += GetStringPadR(pokeDetail.is_default ? "true" : "false", 15);
            fixedRow += GetStringFromInt(pokeDetail.order, 40);
            fixedRow += GetStringFromInt(pokeDetail.weight, 60);

            stringBuild.AppendLine(fixedRow);

            return stringBuild;
        }

        private static string GetStringPadR(string value, int length, char pad = '|')
        {
            return value == null ? "".PadRight(length, ' ') : value.PadRight(length, pad).Substring(0, length);

        }

        private static string GetStringFromInt(int value, int length, char pad = '|')
        {
            return GetStringPadR(value.ToString(), length, pad);
        }
    }
}
