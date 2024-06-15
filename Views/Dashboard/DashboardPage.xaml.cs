using Microsoft.Maui.Controls;
using System;

namespace VidyamAcademy.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
        private System.Timers.Timer _carouselTimer;
        private readonly ApiService _apiService;

        public DashboardPage(DashboardPageViewModel viewModel, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _apiService = apiService;
            StartCarousel();
        }

        private void StartCarousel()
        {
            _carouselTimer = new System.Timers.Timer(2000);
            _carouselTimer.Elapsed += (sender, e) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    if (imageCarousel.Position == ((DashboardPageViewModel)BindingContext).ImageItems.Count - 1)
                    {
                        imageCarousel.Position = 0;
                    }
                    else
                    {
                        imageCarousel.Position += 1;
                    }
                });
            };
            _carouselTimer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _carouselTimer?.Stop();
        }

        private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
            {
                try
                {
                    await Navigation.PushAsync(new CourseDetailPage(selectedCourse, _apiService));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in OnCourseSelected: {ex.Message}");
                }
                finally
                {
                    ((CollectionView)sender).SelectedItem = null;
                }
            }
        }
    }
}
