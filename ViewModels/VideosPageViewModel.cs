using System.Collections.Generic;
using System.Windows.Input;
using VidyamAcademy.Models;
using VidyamAcademy.Services; // Assuming this is where ApiService is defined
using System.Threading.Tasks;

namespace VidyamAcademy.ViewModels
{
    public partial class VideosPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private List<Video> videos;

        private readonly ApiService _apiService;
        public Subject SelectedSubject { get; }

        public VideosPageViewModel(ApiService apiService, Subject selectedSubject)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            SelectedSubject = selectedSubject ?? throw new ArgumentNullException(nameof(selectedSubject));

            Title = selectedSubject.Name;
            Videos = selectedSubject.Videos ?? new List<Video>();

            // Load videos from API
            LoadVideosAsync();
        }

        private async Task LoadVideosAsync()
        {
            var videos = await _apiService.GetVideosBySubjectAsync(SelectedSubject.SubjectId);
            if (videos != null)
            {
                Videos = videos;
            }
        }

        public ICommand PayNowCommand => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SubjectPaymentPage(SelectedSubject));
        });
    }
}
