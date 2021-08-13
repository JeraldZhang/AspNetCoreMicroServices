using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LocationReporter.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly ILogger _logger;

        private HttpClient _httpClient;

        public HttpTeamServiceClient(
            IOptions<TeamServiceOptions> serviceOptions,
            ILogger<HttpTeamServiceClient> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var url = serviceOptions.Value.Url;

            _logger.LogInformation("Team Service HTTP client using URL {0}", url);

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public async Task<Guid> GetTeamForMember(Guid memberId)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync($"/members/{memberId}/team");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var teamIdResponse = JsonConvert.DeserializeObject<TeamIDResponse>(json);
                return teamIdResponse.TeamID;
            }

            return Guid.Empty;
        }
    }

    public class TeamIDResponse
    {
        public Guid TeamID { get; set; }
    }
}