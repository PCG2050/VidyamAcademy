using Microsoft.Maui.Controls;
using System;
using System.Linq;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels.Dashboard;

namespace VidyamAcademy.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
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
            var _carouselTimer = new System.Timers.Timer(2000);
            _carouselTimer.Elapsed += (sender, e) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    var viewModel = (DashboardPageViewModel)BindingContext;
                    if (imageCarousel.Position == viewModel.ImageItems.Count - 1)
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
            // Stop the carousel timer if needed
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

        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchBar = sender as SearchBar;
            if (searchBar != null)
            {
                ((DashboardPageViewModel)BindingContext).SearchCourses(searchBar.Text);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ((DashboardPageViewModel)BindingContext).SearchCourses(e.NewTextValue);
        }
    }
}
