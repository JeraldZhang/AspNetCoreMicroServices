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

		public IEnumerable<Team> List()
		{
			return teams;
		}

		public Team Get(Guid id)
		{
			return teams.FirstOrDefault(t => t.ID == id);
		}

		public Team Update(Team t)
		{
			Team team = this.Delete(t.ID);

			if (team != null)
			{
				team = this.Add(t);
			}

			return team;
		}

		public Team Add(Team team)
		{
			teams.Add(team);
			return team;
		}

		public Team Delete(Guid id)
		{
			var q = teams.Where(t => t.ID == id);
			Team team = null;

			if (q.Count() > 0)
			{
				team = q.First();
				teams.Remove(team);
			}

			return team;
		}
	}
}
