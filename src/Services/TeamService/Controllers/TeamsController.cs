using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamService.Models;
using TeamService.Persisitence;

namespace TeamService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController : ControllerBase
    {
        protected readonly ITeamRepository repository;

        public TeamsController(ITeamRepository teamRepository)
        {
            repository = teamRepository;
        }

        [HttpGet]
        public async virtual Task<IActionResult> GetAllTeams()
        {
            return Ok(await repository.GetTeams());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            
            await repository.AddTeam(team);
            return Ok();
        }
    }
}
