using System;
using System.Collections.Generic;
using System.Linq;
using LocationService.Models;
using LocationService.Persistence;

namespace LocationService.Persisitence
{
    public class LocationRecordRepository : ILocationRecordRepository
    {
        private readonly LocationRecordDbContext _teamContext;
        public LocationRecordRepository(LocationRecordDbContext teamContext)
        {
            _teamContext = teamContext ?? throw new ArgumentNullException(nameof(teamContext));
        }

        public LocationRecord Add(LocationRecord record)
        {
            _teamContext.LocationRecords.Add(record);
            _teamContext.SaveChanges();
            return record;
        }

        public LocationRecord Update(LocationRecord record)
        {
            _teamContext.LocationRecords.Update(record);
            _teamContext.SaveChanges();
            return record;
        }

        public LocationRecord Delete(LocationRecord record)
        {
            _teamContext.Remove(record);
            _teamContext.SaveChanges();
            return record;
        }

        public LocationRecord Get(Guid memeberId, Guid recordId)
        {
            return _teamContext.LocationRecords
                .Single(lr => lr.MemberID == memeberId && lr.ID == recordId);
        }

        public LocationRecord GetLatestForMemeber(Guid memeberId)
        {
            return _teamContext.LocationRecords.Where(lr => lr.MemberID == memeberId)
                .OrderBy(lr => lr.Timestamp).LastOrDefault();
        }

        public ICollection<LocationRecord> AllForMemeber(Guid memeberId)
        {
            return _teamContext.LocationRecords.Where(lr => lr.MemberID == memeberId)
                .OrderBy(lr => lr.Timestamp).ToList();
        }
    }
}
