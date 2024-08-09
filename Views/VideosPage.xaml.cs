using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels;

namespace VidyamAcademy.Views
{
    public partial class VideosPage : ContentPage
    {
        private ImageButton _previousButton;
        public VideosPage(ApiService apiService, Subject selectedSubject)
        {
            InitializeComponent();

            if (selectedSubject == null)
            {
                throw new ArgumentNullException(nameof(selectedSubject), "Selected subject cannot be null.");
            }

            var viewModel = new VideosPageViewModel(apiService, selectedSubject);
            BindingContext = viewModel;
      
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is VideosPageViewModel viewModel)
            {
                await viewModel.RefreshSubjectStatusAsync();
                viewModel.LoadVideosCommand.Execute(null);
            }
        }     

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mediaElement.Pause();
        }

        private void OnThumbnailTapped(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton)
            {
                var video = (Video)imageButton.BindingContext;
                if (video == null)
                {
                    return;
                }

                if (video.IsFree || !string.IsNullOrEmpty(video.SasToken))
                {
                    HighlightButton(imageButton);

                    mediaElement.IsVisible = true;
                    mediaElement.Source = new Uri(video.EffectiveUrl);
                    mediaElement.Play();
                    //Updating the video title
                    if (BindingContext is VideosPageViewModel viewModel)
                    {
                        viewModel.CurrentlyPlayingTitle = video.Title;
                        viewModel.IsMediaElementVisible = true;
                    }

                }
                else
                {
                    DisplayAlert("Pay Now", $"You need to pay to access this video. Pay now for subject: {((VideosPageViewModel)BindingContext).SelectedSubject.Name} to watch this video", "OK");
                }
            }
        }

        private void HighlightButton(ImageButton currentButton)
        {
            if (_previousButton != null)
            {
                _previousButton.Opacity = 1.0;
            }

            currentButton.Opacity = 0.5;

            _previousButton = currentButton;
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                button.BackgroundColor = Colors.DarkGray;
                button.Scale = 0.8;
                Task.Delay(200).ContinueWith(_ => button.Scale = 1.0, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchBar = sender as SearchBar;
            if (searchBar != null)
            {
                ((VideosPageViewModel)BindingContext).SearchVideos(searchBar.Text);
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ((VideosPageViewModel)BindingContext).SearchVideos(e.NewTextValue);
        }
    }
}
