using System;
using System.Collections.Generic;
using LocationService.Models;

namespace LocationService.Persisitence.InMemoryImp
{
    public class InMemoryLocationRecordRepository : ILocationRecordRepository
    {
        protected static ICollection<LocationRecord> _locationRecords;

        public InMemoryLocationRecordRepository()
        {
            if (_locationRecords is null)
                _locationRecords = new List<LocationRecord>();
        }

        public InMemoryLocationRecordRepository(ICollection<LocationRecord> locationRecords)
        {
            _locationRecords = locationRecords ??
                throw new ArgumentNullException(nameof(locationRecords));
        }

        public LocationRecord Add(LocationRecord record)
        {
            _locationRecords.Add(record);
            return record;
        }

        public ICollection<LocationRecord> AllForMemeber(Guid memeberId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord Delete(LocationRecord record)
        {
            if (_locationRecords.Contains(record))
                _locationRecords.Remove(record);
            return record;
        }

        public LocationRecord Get(Guid memeberId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord Get(Guid memeberId, Guid recordId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord GetLatestForMemeber(Guid memeberId)
        {
            throw new NotImplementedException();
        }

        public LocationRecord Update(LocationRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
