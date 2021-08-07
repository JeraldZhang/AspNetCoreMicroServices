using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationService.Models;

namespace LocationService.Persisitence
{
    public interface ILocationRecordRepository
    {
        LocationRecord Add(LocationRecord record);
        LocationRecord Update(LocationRecord record);
        LocationRecord Delete(LocationRecord record);
        LocationRecord Get(Guid memeberId, Guid recordId);
        LocationRecord GetLatestForMemeber(Guid memeberId);
        ICollection<LocationRecord> AllForMemeber(Guid memeberId);
    }
}
