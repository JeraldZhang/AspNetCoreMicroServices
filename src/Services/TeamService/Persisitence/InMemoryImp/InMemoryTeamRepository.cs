using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Persisitence.InMemoryImp
{
    public class InMemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> teams;

        public InMemoryTeamRepository()
        {
            if (teams == null)
                teams = new List<Team>();
        }

        public InMemoryTeamRepository(ICollection<Team> teams)
        {
            InMemoryTeamRepository.teams = teams;
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
