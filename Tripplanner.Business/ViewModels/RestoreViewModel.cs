using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class RestoreViewModel : ViewModelBase
    {
        private IBackupService backupService;

        public RestoreViewModel(IBackupService backupService)
        {
            this.backupService = backupService;
            IndeterminateLoading = true;
        }

        public bool IndeterminateLoading { get; }

        private ObservableCollection<string> _backups;
        public ObservableCollection<string> Backups
        {
            get => _backups;
            set
            {
                _backups = value;
                RaisePropertyChanged(() => Backups);
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public override async Task Initialize()
        {
            await LoadBackups();
            await base.Initialize();
        }

        private async Task LoadBackups()
        {
            IsLoading = true;
            var backupNames = await backupService.GetBackupNames();
            Backups = new ObservableCollection<string>(backupNames);
            IsLoading = false;
        }
    }
}
