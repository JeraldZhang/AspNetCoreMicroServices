using System;
using System.Threading.Tasks;

namespace LocationReporter.Services
{
    public interface ITeamServiceClient
    {
        Task<Guid> GetTeamForMember(Guid memberId);
    }
}