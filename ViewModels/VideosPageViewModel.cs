using System.Collections.ObjectModel;
using System.Linq;
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

        private ObservableCollection<Video> _allVideos;

        private readonly ApiService _apiService;
        public Subject SelectedSubject { get; }

        public VideosPageViewModel(ApiService apiService, Subject selectedSubject)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            SelectedSubject = selectedSubject ?? throw new ArgumentNullException(nameof(selectedSubject));

            Title = selectedSubject.Name;
            _allVideos = new ObservableCollection<Video>(selectedSubject.Videos ?? new List<Video>());
            Videos = new ObservableCollection<Video>(_allVideos);
            LoadVideosCommand = new AsyncRelayCommand(LoadVideosAsync);
        }

        private async Task LoadVideosAsync()
        {
            var videos = await _apiService.GetVideosBySubjectAsync(SelectedSubject.SubjectId);
            if (videos != null)
            {
                _allVideos.Clear();
                foreach (var video in videos)
                {
                    _allVideos.Add(video);
                }
                Videos = new ObservableCollection<Video>(_allVideos);
                OnPropertyChanged(nameof(Videos));
            }
        }

        public ICommand LoadVideosCommand { get; }

        public void SearchVideos(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Videos = new ObservableCollection<Video>(_allVideos);
            }
            else
            {
                var filteredVideos = _allVideos.Where(v => v.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                Videos = new ObservableCollection<Video>(filteredVideos);
            }
            OnPropertyChanged(nameof(Videos));
        }

        public ICommand PayNowCommand => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SubjectPaymentPage(SelectedSubject, _apiService));
        });

        public bool IsPayNowButtonEnabled => SelectedSubject.IsPayNowButtonEnabled;
    }
}
