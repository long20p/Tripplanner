using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;

namespace Tripplanner.Business.ViewModels
{
    public class CustomGuideViewModel : ViewModelBase
    {
        private ICustomGuideNoteRepository customGuideNoteRepository;

        public CustomGuideViewModel(ICustomGuideNoteRepository repository)
        {
            customGuideNoteRepository = repository;
            AddNewEntryCommand = GetCommand(AddNewEntry);
        }

        public Trip Trip { get; set; }

        public ICommand AddNewEntryCommand { get; }

        private string newEntryText;
        public string NewEntryText
        {
            get => newEntryText;
            set 
            {
                newEntryText = value;
                RaisePropertyChanged(() => NewEntryText);
            }
        }

        private ObservableCollection<CustomGuideNote> notes;
        public ObservableCollection<CustomGuideNote> Notes 
        {
            get => notes;
            set
            {
                notes = value;
                RaisePropertyChanged(() => Notes);
            }
        }

        public void LoadTripNotes(Trip trip)
        {
            Trip = trip;
            var tripNotes = customGuideNoteRepository.Where(x => x.TripId == Trip.UniqueId).ToList();
            Notes = new ObservableCollection<CustomGuideNote>(tripNotes);
        }

        private void AddNewEntry()
        {
            if(string.IsNullOrWhiteSpace(NewEntryText))
            {
                return;
            }

            var newNote = new CustomGuideNote { TripId = Trip.UniqueId, UniqueId = Guid.NewGuid(), NoteText = NewEntryText };
            customGuideNoteRepository.Add(newNote);
            Notes.Add(newNote);
            RaisePropertyChanged(() => Notes);
            NewEntryText = string.Empty;
        }
    }
}
