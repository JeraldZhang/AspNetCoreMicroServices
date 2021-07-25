using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamService.Models;
using TeamService.Persisitence;

namespace TeamService.Controllers
{
    public class TeamsController : Controller
    {
        ITeamRepository repository;

        public TeamsController(ITeamRepository teamRepository)
        {
            repository = teamRepository;
        }

        [HttpGet]
        public async virtual Task<IActionResult> GetAllTeams()
        {
            return Ok(await repository.GetTeams());
        }

        public async Task<IActionResult> CreateTeam(Team team)
        {
            await repository.AddTeam(team);
            return Ok();
        }
    }
}
