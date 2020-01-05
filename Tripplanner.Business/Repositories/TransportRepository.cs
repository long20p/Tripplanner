using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public class TransportRepository : BaseRepository<LongDistanceTransport>, ITransportRepository
    {
        public TransportRepository(IStorageService storageService) : base(storageService)
        {
        }
    }
}
