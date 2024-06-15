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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as VideosPageViewModel;
         
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mediaElement.Pause();
        }

        private void OnThumbnailTapped(object sender, EventArgs e)
        {
            if (sender is ImageButton imageButton && imageButton.CommandParameter is string videoUrl)
            {
                var video = (Video)imageButton.BindingContext; // Assuming Video object is BindingContext
                if (video.IsFree)
                {
                    // Highlight the current button
                    HighlightButton(imageButton);

                    // Use EffectiveUrl for the MediaElement source
                    mediaElement.IsVisible = true;
                    mediaElement.Source = new Uri(video.EffectiveUrl);
                    mediaElement.Play();
                }
                else
                {
                    // Display alert and prevent playback (if not free)
                    DisplayAlert("Pay Now", $"You need to pay to access this video. Pay now for subject: {((VideosPageViewModel)BindingContext).SelectedSubject.Name} to watch this video", "OK");
                }
            }
        }

        private void HighlightButton(ImageButton currentButton)
        {
            // Reset the opacity of the previously highlighted button
            if (_previousButton != null)
            {
                _previousButton.Opacity = 1.0;
            }

            // Highlight the current button by reducing its opacity
            currentButton.Opacity = 0.5;

            // Store the current button as the previous button for future reference
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
    }
}
