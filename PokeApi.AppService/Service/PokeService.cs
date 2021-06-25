using PokeApi.AppService.Dto;
using PokeApi.AppService.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.AppService.Service
{
    public class PokeService : HttpHelper, IPokeService
    {
        public PokeService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<ServiceResult<List<PokeDto>>> SearchPokeByFilterNameAsync(string filterName)
        {
            var result = new ServiceResult<List<PokeDto>>();
            var data = new List<PokeDto>();

            if (string.IsNullOrEmpty(filterName))
            {
                result.AddErrorMessage("Error, debe de enviar un pokemon");
                return result;
            }

            var resultallPokeCount = await MakeHttpGetRequest<AllPokeCount>(AppSetting.BaseUrl);
            var count = resultallPokeCount.Data.count;
            
            var allPoke = await MakeHttpGetRequest<PokemonsResult>($"{AppSetting.BaseUrl}?limit={count}");

            var pokeResult = allPoke.Data.results
                .Where(x => x.name.Contains(filterName))
                .ToList();

            foreach (var poke in pokeResult)
            {
                var GetPokeByNameResult = await MakeHttpGetRequest<PokeDto>($"{AppSetting.BaseUrl}{poke.name}");

                data.Add(GetPokeByNameResult.Data);
            }

            result.Data = data;

            if (result.Data.Count == 0)
            {
                result.AddErrorMessage("No se ha Encontrado ningun Pokemon");
                return result;
            }

            return result;
        }

        private async Task<ServiceResult<PokeDto>> GetPokeByIdAsync(int id)
        {
            var result = await MakeHttpGetRequest<PokeDto>($"{AppSetting.BaseUrl}{id}");

            if (result.Data.id == 0)
            {
                result.AddErrorMessage("Se ha detectado un error. Contacte con su administrador");
                return result;
            }

            return result;
        }

        public ServiceResult<ReportFileResponse> DownloadDetail(int id)
        {
            var result = new ServiceResult<ReportFileResponse>();

            var pokeData = GetPokeByIdAsync(id).Result;

            var fixedRow = pokeData.Data.ToFixedRow();

            var pokeName = pokeData.Data != null ? pokeData.Data.name : "";

            var filename = $"Detalle_del_pokemon_{pokeName}.txt";

            result.Data = new ReportFileResponse
            {
                FileContent = Encoding.ASCII.GetBytes(fixedRow.ToString()),
                Filename = filename
            };

            return result;
        }
    }

    public interface IPokeService
    {
        Task<ServiceResult<List<PokeDto>>> SearchPokeByFilterNameAsync(string filterName);
        ServiceResult<ReportFileResponse> DownloadDetail(int id);
    }
}
