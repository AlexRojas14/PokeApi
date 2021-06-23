using PokeApi.AppService.Dto;
using PokeApi.AppService.Framework;
using System.Net.Http;
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

        public async Task GetPokeByNameAsync(string name)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{AppSetting.BaseUrl}{name}");

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var pullRequests = await JsonSerializer.DeserializeAsync<PokeDto>(responseStream);
            }
        }

        public string[] prueba()
        {
            string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            return Summaries;
        }
    }

    public interface IPokeService
    {
        Task GetPokeByNameAsync(string name);
        string[] prueba();
    }
}
