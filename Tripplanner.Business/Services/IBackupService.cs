using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Configs;

namespace Tripplanner.Business.Services
{
    public interface IBackupService
    {
        Task<bool> BackupAllTrips(string backupName);
        Task<bool> BackupSelectedTrips(IEnumerable<Guid> tripIds, string backupName);
        Task<IEnumerable<string>> GetBackupNames();
        Task RestoreFromLocalStorage(IEnumerable<ArchiveEntry> files);
        Task<TripsExistenceCheckResult> CheckBackupTripExists(string backupName);
    }
}
