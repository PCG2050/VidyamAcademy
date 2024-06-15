using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace VidyamAcademy.ViewModels
{
    public partial class VideosPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private ObservableCollection<Video> videos;

        private readonly ApiService _apiService;
        public Subject SelectedSubject { get; }

        public VideosPageViewModel(ApiService apiService, Subject selectedSubject)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            SelectedSubject = selectedSubject ?? throw new ArgumentNullException(nameof(selectedSubject));

            Title = selectedSubject.Name;
            Videos = new ObservableCollection<Video>(selectedSubject.Videos ?? new List<Video>());
            LoadVideosAsync();
        }

        private async Task LoadVideosAsync()
        {
            var videos = await _apiService.GetVideosBySubjectAsync(SelectedSubject.SubjectId);
            if (videos != null)
            {
                Videos.Clear();
                foreach (var video in videos)
                {
                    Videos.Add(video);
                }
            }
        }

        public ICommand PayNowCommand => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SubjectPaymentPage(SelectedSubject));
        });
        public bool IsPayNowButtonEnabled => SelectedSubject.IsPayNowButtonEnabled;
    }
}
