using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tripplanner.Business.Services
{
    public interface IBackupService
    {
        Task<bool> BackupAllTrips(string backupName);
        Task<bool> BackupSelectedTrips(IEnumerable<Guid> tripIds, string backupName);
    }
}
