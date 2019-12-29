using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public class ExchangeRateRepository : BaseRepository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(IStorageService storageService) : base(storageService)
        {
        }
    }
}
