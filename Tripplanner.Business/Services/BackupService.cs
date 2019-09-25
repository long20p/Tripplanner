using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Configs;
using Tripplanner.Business.Models;
using Tripplanner.Business.Utils;

namespace Tripplanner.Business.Services
{
    public class BackupService : IBackupService
    {
        private ITripRepository tripRepository;
        private IAccommodationRepository accommodationRepository;
        private IStorageService storageService;
        private ISerializer serializer;

        public BackupService(IStorageService storageService, ITripRepository tripRepository,
            IAccommodationRepository accommodationRepository, ISerializer serializer)
        {
            this.storageService = storageService;
            this.tripRepository = tripRepository;
            this.accommodationRepository = accommodationRepository;
            this.serializer = serializer;
        }

        public async Task<bool> BackupAllTrips(string backupName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var trips = serializer.Serialize(tripRepository.GetAll());
                    var accommodations = serializer.Serialize(accommodationRepository.GetAll());

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

        public async Task<IEnumerable<string>> GetBackupNames()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return storageService.GetFilesInFolder(Constants.FolderBackup, false);
                }
                catch (Exception ex)
                {
                    //TODO: Logging...
                    return null;
                }
            });
        }

        public async Task RestoreFromLocalStorage(IEnumerable<ArchiveEntry> files)
        {
            await Task.Run(() =>
            {
                var tripFile = files.FirstOrDefault(x => x.Name == Constants.BackupTripsFileName);
                var accommFile = files.FirstOrDefault(x => x.Name == Constants.BackupAccommodationsFileName);
                var trips = serializer.Deserialize<IEnumerable<Trip>>(tripFile.Content as string);
                var accommodations = serializer.Deserialize<IEnumerable<Accommodation>>(accommFile.Content as string);
                tripRepository.AddOrReplace(trips);
                accommodationRepository.AddOrReplace(accommodations);
            });
        }

        public async Task<TripsExistenceCheckResult> CheckBackupTripExists(string backupName)
        {
            return await Task.Run(() =>
            {
                var entries = storageService.GetZipContents(Path.Combine(Constants.FolderBackup, $"{backupName}.zip"));
                var tripFile = entries.FirstOrDefault(x => x.Name == Constants.BackupTripsFileName);
                var trips = serializer.Deserialize<IEnumerable<Trip>>(tripFile.Content as string);
                var exists = tripRepository.GetAll().Select(x => x.UniqueId).Intersect(trips.Select(x => x.UniqueId)).Any();
                return new TripsExistenceCheckResult {AlreadyExists = exists, Entries = entries};
            });
        }
    }
}
