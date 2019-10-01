using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class BackupViewModel : ViewModelBase
    {
        private Dictionary<string, Expression<Func<bool>>> choiceMappings;
        private IBackupService backupService;
        private INotificationService notificationService;

        public BackupViewModel(INotificationService notificationService, IBackupService backupService)
        {
            this.notificationService = notificationService;
            this.backupService = backupService;
            choiceMappings = new Dictionary<string, Expression<Func<bool>>>
            {
                {nameof(IsLocalStorageChosen), () => IsLocalStorageChosen},
                {nameof(IsDropboxChosen), () => IsDropboxChosen},
                {nameof(IsAmazonChosen), () => IsAmazonChosen}
            };
            IsAllTripsOptionSelected = true;
            SelectAllTripsCommand = GetCommand(SelectAllTrips);
            SelectCustomTripsCommand = GetCommand(SelectCustomTrips);
            SelectLocalStorageCommand = GetCommand(() => ToggleChoice(nameof(IsLocalStorageChosen), !IsLocalStorageChosen));
            SelectDropboxCommand = GetCommand(() => ToggleChoice(nameof(IsDropboxChosen), !IsDropboxChosen));
            SelectAmazonCommand = GetCommand(() => ToggleChoice(nameof(IsAmazonChosen), !IsAmazonChosen));
            CreateBackupCommand = GetAsyncCommand(async () => await CreateBackup());
        }

        public ICommand SelectAllTripsCommand { get; }
        public ICommand SelectCustomTripsCommand { get; }
        public ICommand SelectLocalStorageCommand { get; }
        public ICommand SelectDropboxCommand { get; }
        public ICommand SelectAmazonCommand { get; }
        public ICommand CreateBackupCommand { get; }

        private bool isAllTripsOptionSelected;

        public bool IsAllTripsOptionSelected
        {
            get => isAllTripsOptionSelected;
            set
            {
                isAllTripsOptionSelected = value;
                RaisePropertyChanged(() => IsAllTripsOptionSelected);
            }
        }

        private bool _isLocalStorageChosen;
        public bool IsLocalStorageChosen
        {
            get => _isLocalStorageChosen;
            set
            {
                _isLocalStorageChosen = value;
                RaisePropertyChanged(() => IsLocalStorageChosen);
            }
        }

        private bool _isDropboxChosen;
        public bool IsDropboxChosen
        {
            get => _isDropboxChosen;
            set
            {
                _isDropboxChosen = value;
                RaisePropertyChanged(() => IsDropboxChosen);
            }
        }

        private bool _isAmazonChosen;
        public bool IsAmazonChosen
        {
            get => _isAmazonChosen;
            set
            {
                _isAmazonChosen = value;
                RaisePropertyChanged(() => IsAmazonChosen);
            }
        }

        private string _localBackupName;
        public string LocalBackupName
        {
            get => _localBackupName;
            set
            {
                _localBackupName = value;
                RaisePropertyChanged(() => LocalBackupName);
            }
        }

        private void SelectAllTrips()
        {
            IsAllTripsOptionSelected = true;
        }

        private void SelectCustomTrips()
        {
            IsAllTripsOptionSelected = false;
        }

        private void ToggleChoice(string choiceName, bool value)
        {
            SetChoiceValue(choiceName, value);
            foreach (var choice in choiceMappings.Where(x => x.Key != choiceName))
            {
                SetChoiceValue(choice.Key, false);
            }
        }

        private void SetChoiceValue(string choiceName, bool value)
        {
            var propExpr = choiceMappings[choiceName];
            var prop = (propExpr.Body as MemberExpression)?.Member as PropertyInfo;
            if (prop != null && prop.PropertyType == typeof(bool))
            {
                prop.SetValue(this, value);
            }
        }

        private async Task CreateBackup()
        {
            var backupName = $"{(string.IsNullOrWhiteSpace(LocalBackupName) ? Constants.DefaultBackupFileName : LocalBackupName)}_{DateTime.Now:yyyyMMdd_hhmmss}";
            if (IsAllTripsOptionSelected)
            {
                var succeed = await backupService.BackupAllTrips(backupName);
                if (succeed)
                {
                    notificationService.ShowInfo($"Backup created successfully");
                }
            }
            else
            {
                
            }
        }
    }
}
