using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;
using VidyamAcademy.Models;
using VidyamAcademy.Services;

namespace VidyamAcademy.ViewModels.Dashboard
{
    public class DashboardPageViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Course> _allCourses;

        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<ImageItem> ImageItems { get; set; }

        public DashboardPageViewModel(ApiService apiService)
        {
            _apiService = apiService;
            InitializeImages();
            LoadCourseData();
        }

        public async void LoadCourseData()
        {
            var courseDetails = await _apiService.GetCourseDetailsAsync();
            _allCourses = new ObservableCollection<Course>(courseDetails);
            Courses = new ObservableCollection<Course>(_allCourses);
            OnPropertyChanged(nameof(Courses));
        }

        private void InitializeImages()
        {
            ImageItems = new ObservableCollection<ImageItem>
            {
                new ImageItem { ImageSource = "dbimage1.jpg" },
                new ImageItem { ImageSource = "dbimage2.jpg" },
                new ImageItem { ImageSource = "dbimage3.jpg" }
            };
        }

        public void SearchCourses(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Courses = new ObservableCollection<Course>(_allCourses);
            }
            else
            {
                var filteredCourses = _allCourses.Where(c => c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                Courses = new ObservableCollection<Course>(filteredCourses);
            }
            OnPropertyChanged(nameof(Courses));
        }

        public class ImageItem
        {
            public string ImageSource { get; set; }
        }
    }
}
