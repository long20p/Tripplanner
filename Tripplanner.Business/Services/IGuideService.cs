using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.Services
{
    public interface IGuideService
    {
        Task<IEnumerable<GuideSection>> GetAllSections(string location);
        Task<string> GetSectionByIndex(string location, int index);
        Task<IEnumerable<string>> GetSuggestions(string searchTerm);
    }
}
