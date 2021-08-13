using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.Persisitence
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task AddTeam(Team team);

        IEnumerable<Team> List();
        Team Get(Guid id);
        Team Add(Team team);
        Team Update(Team team);
        Team Delete(Guid id);
    }
}
