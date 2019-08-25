using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Configs;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class RestoreViewModel : ViewModelBase
    {
        private IBackupService backupService;
        private string selectedItem;

        public RestoreViewModel(IBackupService backupService)
        {
            this.backupService = backupService;
            SelectBackupCommand = GetCommand<string>(SelectBackup);
            RestoreCommand = GetAsyncCommand(async () => await Restore());
            IndeterminateLoading = true;
        }

        public ICommand SelectBackupCommand { get; }
        public ICommand RestoreCommand { get; }

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

        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get => _isItemSelected;
            set
            {
                _isItemSelected = value;
                RaisePropertyChanged(() => IsItemSelected);
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
            Backups =  new ObservableCollection<string>(backupNames ?? new List<string>());
            IsLoading = false;
        }

        private void SelectBackup(string name)
        {
            selectedItem = selectedItem == name ? null : name;
            IsItemSelected = selectedItem != null;
        }

        private async Task Restore()
        {
            IsLoading = true;
            var existenceCheckResult = await backupService.CheckBackupTripExists(selectedItem);
            IsLoading = false;

            if (existenceCheckResult.AlreadyExists)
            {
                await NavigationService.Navigate<GenericConfirmationViewModel, Confirmation>(new Confirmation
                {
                    Message = "Some items already exist. Do you want to overwrite?",
                    OnCancel = () => { },
                    OnProceed = async () => await RestoreAndMaybeOverwrite(existenceCheckResult.Entries)
                });
                return;
            }

            await RestoreAndMaybeOverwrite(existenceCheckResult.Entries);
        }

        private async Task RestoreAndMaybeOverwrite(IEnumerable<ArchiveEntry> files)
        {
            IsLoading = true;
            await backupService.RestoreFromLocalStorage(files);
            IsLoading = false;
        }
    }
}
