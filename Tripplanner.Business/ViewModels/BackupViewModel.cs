using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class BackupViewModel : ViewModelBase
    {
        private Dictionary<string, Expression<Func<bool>>> choiceMappings;
        private IStorageService storageService;

        public BackupViewModel(IStorageService storageService)
        {
            this.storageService = storageService;
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
        }

        public ICommand SelectAllTripsCommand { get; }
        public ICommand SelectCustomTripsCommand { get; }
        public ICommand SelectLocalStorageCommand { get; }
        public ICommand SelectDropboxCommand { get; }
        public ICommand SelectAmazonCommand { get; }

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
    }
}
