using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace VidyamAcademy.ViewModels
{
    public class SubjectsPageViewModel : BindableObject
    {
        private Subject _previousSelectedSubject;

        public ObservableCollection<Subject> Subjects { get; set; }

        // Parameterless constructor for XAML
        public SubjectsPageViewModel()
        {
            Subjects = new ObservableCollection<Subject>();
        }

        public SubjectsPageViewModel(List<Subject> subjects)
        {
            Subjects = new ObservableCollection<Subject>(subjects);
        }

        public ICommand SubjectSelectedCommand => new Command<Subject>((selectedSubject) =>
        {
            // Toggle the selected item's expanded state
            if (selectedSubject.IsExpanded)
            {
                selectedSubject.IsExpanded = false;
            }
            else
            {
                // Collapse the previously selected item, if any
                if (_previousSelectedSubject != null && _previousSelectedSubject != selectedSubject)
                {
                    _previousSelectedSubject.IsExpanded = false;
                }
                selectedSubject.IsExpanded = true;
            }

            // Update the previous selected item reference
            _previousSelectedSubject = selectedSubject.IsExpanded ? selectedSubject : null;

            // Notify that the collection has been updated to refresh the ListView
            OnPropertyChanged(nameof(Subjects));
        });

        public ICommand GoToVideosCommand => new Command<Subject>(async (subject) =>
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PushAsync(new VideosPage(subject));
        });

        public ICommand PayNowCommand => new Command<Subject>(async (subject) =>
        {
            await Application.Current.MainPage.DisplayAlert("Payment", $"Payment initiated for {subject.Name}", "OK");
        });
    }
}
