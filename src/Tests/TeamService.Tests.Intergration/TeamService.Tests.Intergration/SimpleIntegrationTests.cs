using AspNetCoreMicroServices.Services.TeamService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamService.Models;
using Xunit;

namespace TeamService.Tests.Intergration
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;

        private readonly Team teamZombie;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();

            teamZombie = new Team()
            {
                ID = Guid.NewGuid(),
                Name = "Zombie"
            };
        }

        [Fact]
        public async Task TestTeamPostAndGet()
        {
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(teamZombie), Encoding.UTF8, "application/json");

            var postResponse = await testClient.PostAsync("/teams", stringContent);
            postResponse.EnsureSuccessStatusCode();
            var getResponse = await testClient.GetAsync("/teams");
            getResponse.EnsureSuccessStatusCode();

            var raw = await getResponse.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            Assert.Single(teams);
            Assert.Equal("Zombie", teams[0].Name);
            Assert.Equal(teamZombie.ID, teams[0].ID);
        }
    }
}
