using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(IStorageService storageService) : base(storageService)
        {
        }
    }
}
