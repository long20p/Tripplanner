using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public class CustomGuideNoteRepository : BaseRepository<CustomGuideNote>, ICustomGuideNoteRepository
    {
        public CustomGuideNoteRepository(IStorageService storageService) : base(storageService)
        {
        }
    }
}
