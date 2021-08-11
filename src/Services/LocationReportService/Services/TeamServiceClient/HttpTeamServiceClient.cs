using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
            this._logger = logger;

            var url = serviceOptions.Value.Url;

            logger.LogInformation("Team Service HTTP client using URL {0}", url);

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
        }

        public Guid GetTeamForMember(Guid memberId)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _httpClient.GetAsync($"/members/{memberId}/team").Result;

            TeamIDResponse teamIdResponse;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                teamIdResponse = JsonConvert.DeserializeObject<TeamIDResponse>(json);
                return teamIdResponse.TeamID;
            }
            else
            {
                return Guid.Empty;
            }
        }
    }

    public class TeamIDResponse
    {
        public Guid TeamID { get; set; }
    }
}