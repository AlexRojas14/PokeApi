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

        public async Task<ServiceResult<List<PokeDto>>> GetPokeByNameAsync(string name)
        {
            var result = new ServiceResult<List<PokeDto>>();
            var data = new List<PokeDto>();

            if (string.IsNullOrEmpty(name))
            {
                result.AddErrorMessage("Error, debe de enviar un pokemon");
                return result;
            }

            var makeHttpGetResponse = await MakeHttpGetRequest<PokeDto>($"{AppSetting.BaseUrl}{name}");

            if (!makeHttpGetResponse.ExecutedSuccesfully)
            {
                return await SearchPokeByFilterNameAsync(name);
            }

            data.Add(makeHttpGetResponse.Data);
            result.Data = data;

            if (result.Data.Count == 0)
            {
                result.AddErrorMessage("Se ha detectado un error. Contacte con su administrador");
                return result;
            }

            return result;
        }

        private async Task<ServiceResult<List<PokeDto>>> SearchPokeByFilterNameAsync(string filterName)
        {
            var result = new ServiceResult<List<PokeDto>>();
            var data = new List<PokeDto>();

            var resultallPokeCount = await MakeHttpGetRequest<AllPokeCount>(AppSetting.BaseUrl);
            var count = resultallPokeCount.Data.count;
            
            var allPoke = await MakeHttpGetRequest<PokemonsResult>($"{AppSetting.BaseUrl}?limit={count}");

            var pokeResult = allPoke.Data.results
                .Where(x => x.name.Contains(filterName))
                .ToList();

            foreach (var poke in pokeResult)
            {
                var GetPokeByNameResult = await GetPokeByNameAsync(poke.name);
                data.AddRange(GetPokeByNameResult.Data);
            }

            result.Data = data;

            if (result.Data.Count == 0)
            {
                result.AddErrorMessage("Se ha detectado un error. Contacte con su administrador");
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
        Task<ServiceResult<List<PokeDto>>> GetPokeByNameAsync(string name);
        ServiceResult<ReportFileResponse> DownloadDetail(int id);
    }
}
