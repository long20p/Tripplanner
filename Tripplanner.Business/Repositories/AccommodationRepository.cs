using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public class AccommodationRepository : BaseRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(IStorageService storageService) : base(storageService)
        {
        }
    }
}
