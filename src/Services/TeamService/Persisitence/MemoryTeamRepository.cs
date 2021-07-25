using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Persisitence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public MemoryTeamRepository()
        {
            if (teams == null)
                teams = new List<Team>();
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            MemoryTeamRepository.teams = teams;
        }

        public Task AddTeam(Team team)
        {
            teams.Add(team);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Team>> GetTeams()
        {
            return Task.FromResult(teams.AsEnumerable());
        }
    }
}
