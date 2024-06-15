using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using VidyamAcademy.Models;
using VidyamAcademy.Services;

namespace VidyamAcademy.ViewModels
{
    public class SubjectsPageViewModel : BindableObject
    {
        private Subject _previousSelectedSubject;
        private readonly ApiService _apiService;

        public ObservableCollection<Subject> Subjects { get; set; }

        public SubjectsPageViewModel(ApiService apiService, int courseId)
        {
            _apiService = apiService;
            Subjects = new ObservableCollection<Subject>();
            LoadSubjects(courseId);
        }

        public SubjectsPageViewModel()
        {
            Subjects = new ObservableCollection<Subject>();
        }

        private async void LoadSubjects(int courseId)
        {
            var subjects = await _apiService.GetSubjectsByCourseAsync(courseId);
            if (subjects != null)
            {
                foreach (var subject in subjects)
                {
                    Subjects.Add(subject);
                }
            }
        }

        public ICommand SubjectSelectedCommand => new Command<Subject>((selectedSubject) =>
        {
            if (selectedSubject == null)
                return;

            if (selectedSubject.IsExpanded)
            {
                selectedSubject.IsExpanded = false;
            }
            else
            {
                if (_previousSelectedSubject != null && _previousSelectedSubject != selectedSubject)
                {
                    _previousSelectedSubject.IsExpanded = false;
                }
                selectedSubject.IsExpanded = true;
            }

            _previousSelectedSubject = selectedSubject.IsExpanded ? selectedSubject : null;
            OnPropertyChanged(nameof(Subjects));
        });

        public ICommand GoToVideosCommand => new Command<Subject>(async (subject) =>
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PushAsync(new VideosPage(_apiService, subject));
        });

        public ICommand PayNowCommand => new Command<Subject>(async (subject) =>
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PushModalAsync(new SubjectPaymentPage(subject));
        });
    }
}
