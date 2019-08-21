using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Repositories;
using Newtonsoft.Json;
using Tripplanner.Business.Configs;

namespace Tripplanner.Business.Services
{
    public class BackupService : IBackupService
    {
        private ITripRepository tripRepository;
        private IAccommodationRepository accommodationRepository;
        private IStorageService storageService;

        public BackupService(IStorageService storageService, ITripRepository tripRepository, IAccommodationRepository accommodationRepository)
        {
            this.storageService = storageService;
            this.tripRepository = tripRepository;
            this.accommodationRepository = accommodationRepository;
        }

        public async Task<bool> BackupAllTrips(string backupName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var trips = JsonConvert.SerializeObject(tripRepository.GetAll());
                    var accommodations = JsonConvert.SerializeObject(accommodationRepository.GetAll());

                    var archiveEntries = new List<ArchiveEntry>
                    {
                        new ArchiveEntry(Constants.BackupTripsFileName, ArchiveEntryType.Text, trips),
                        new ArchiveEntry(Constants.BackupAccommodationsFileName, ArchiveEntryType.Text, accommodations)
                    };

                    storageService.SaveZipFile(Path.Combine(Constants.FolderBackup, $"{backupName}.zip"), archiveEntries);
                    return true;
                }
                catch (Exception ex)
                {
                    //TODO: do some error logging here...
                    return false;
                }
            });
        }

        public Task<bool> BackupSelectedTrips(IEnumerable<Guid> tripIds, string backupName)
        {
            throw new NotImplementedException();
        }
    }
}
