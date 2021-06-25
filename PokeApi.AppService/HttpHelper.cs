using PokeApi.AppService.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeApi.AppService
{
    public class HttpHelper
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ServiceResult<T>> MakeHttpGetRequest<T>(string url)
        {
            var result = new ServiceResult<T>();

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                result.AddErrorMessage("No se ha Encontrado ningun Pokemon");
                return result;
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            result.Data = await JsonSerializer.DeserializeAsync<T>(responseStream);

            return result;
        }
    }
}
