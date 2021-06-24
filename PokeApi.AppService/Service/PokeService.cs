using PokeApi.AppService.Dto;
using PokeApi.AppService.Framework;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeApi.AppService.Service
{
    public class PokeService : IPokeService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PokeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ServiceResult<PokeDto>> GetPokeByNameAsync(string name)
        {
            var result = new ServiceResult<PokeDto>();

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{AppSetting.BaseUrl}{name}");

            if (!response.IsSuccessStatusCode)
            {
                result.AddErrorMessage("Se ha detectado un error. Contacte con su administrador");
                return result;
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<PokeDto>(responseStream);

            if (data.id == 0)
            {
                result.AddErrorMessage("No se ha localizado este pokemon");
                return result;
            }

            result.Data = data;

            return result;
        }

        public async Task<ServiceResult<PokeDto>> GetPokeByIdAsync(int id)
        {
            var result = new ServiceResult<PokeDto>();

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{AppSetting.BaseUrl}{id}");

            if (!response.IsSuccessStatusCode)
            {
                result.AddErrorMessage("Se ha detectado un error. Contacte con su administrador");
                return result;
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var data = await JsonSerializer.DeserializeAsync<PokeDto>(responseStream);

            if (data.id == 0)
            {
                result.AddErrorMessage("No se ha localizado este pokemon");
                return result;
            }

            result.Data = data;

            return result;
        }

        public ServiceResult<ReportFileResponse> DownloadDetail(int id)
        {
            var result = new ServiceResult<ReportFileResponse>();

            var pokeData = GetPokeByIdAsync(id).Result;

            var fixedRow = pokeData.Data.ToFixedRow();

            var filename = $"Detalle_del_pokemon_{pokeData.Data.name}.txt";

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
        Task<ServiceResult<PokeDto>> GetPokeByNameAsync(string name);
        ServiceResult<ReportFileResponse> DownloadDetail(int id);
    }
}
