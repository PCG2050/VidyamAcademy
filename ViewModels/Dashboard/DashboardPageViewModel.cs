﻿using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VidyamAcademy.ViewModels.Dashboard
{
    public class DashboardPageViewModel : BindableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<ImageItem> ImageItems { get; set; }

        public DashboardPageViewModel(ApiService apiService)
        {
            _apiService = apiService;
            InitializeImages();
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl(apiService);
            LoadCourseData();
        }

        private async void LoadCourseData()
        {
            var courseDetails = await _apiService.GetCourseDetailsAsync();
            Courses = new ObservableCollection<Course>(courseDetails);
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

        public class ImageItem
        {
            public string ImageSource { get; set; }
        }
    }
}
