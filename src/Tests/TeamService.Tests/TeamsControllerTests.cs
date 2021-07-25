using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamService.Controllers;
using TeamService.Models;
using TeamService.Persisitence;
using Xunit;

namespace AspNetCoreMicroServices.Tests.TeamService.Tests
{
    public class TeamsControllerTests
    {
        [Fact]
        public async Task QueryTeamListReturensCorrectTeams()
        {
            var controller = new TeamsController(new MemoryTeamRepository());

            var teams = new List<Team>((IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value);

            Assert.NotNull(teams);
        }

        [Fact]
        public async void CreateTeamAddsTeamToList()
        {
            var controller = new TeamsController(new MemoryTeamRepository());

            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            var original = new List<Team>(teams);
            var team = new Team("Sample");
            await controller.CreateTeam(team);
            var result = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            var newTeams = new List<Team>(result);

            Assert.Equal(newTeams.Count, original.Count + 1);

            var sampleTeams = newTeams.FirstOrDefault(t => t.Name == "Sample");

            Assert.NotNull(sampleTeams);
        }
    }
}
