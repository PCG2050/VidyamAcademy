using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Timers;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels.Dashboard;

namespace VidyamAcademy.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
        private System.Timers.Timer _carouselTimer;
        private System.Timers.Timer _collectionViewTimer;
        private readonly ApiService _apiService;

        public DashboardPage(DashboardPageViewModel viewModel, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _apiService = apiService;
            StartCarousel();
            StartCollectionViewAutoScroll();
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

        private void StartCollectionViewAutoScroll()
        {
            _collectionViewTimer = new System.Timers.Timer(3000);
            _collectionViewTimer.Elapsed += (sender, e) =>
            {
                Dispatcher.Dispatch(() =>
                {
                    var viewModel = (DashboardPageViewModel)BindingContext;
                    var nextIndex = (coursesCollectionView.ItemsSource.Cast<object>().ToList().IndexOf(coursesCollectionView.SelectedItem) + 1) % viewModel.Courses.Count;
                    coursesCollectionView.ScrollTo(nextIndex);
                });
            };
            _collectionViewTimer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _carouselTimer?.Stop();
            _collectionViewTimer?.Stop();
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
