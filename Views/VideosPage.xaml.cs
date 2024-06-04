namespace VidyamAcademy.Views
{
    public partial class VideosPage : ContentPage
    {
        public List<Video> Videos { get; private set; }
        private ImageButton _previousButton;

        public VideosPage(Subject selectedSubject)
        {
            InitializeComponent();

            if (selectedSubject == null)
            {
                throw new ArgumentNullException(nameof(selectedSubject), "Selected subject cannot be null.");
            }

            Videos = selectedSubject.Videos ?? throw new ArgumentException("Selected subject must have a valid Videos collection.", nameof(selectedSubject));
            var viewModel = new VideosPageViewModel(selectedSubject);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            videosCollectionView.ItemsSource = Videos;
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

                    // Existing logic for handling video playback (if free)
                    mediaElement.IsVisible = true;
                    mediaElement.Source = new Uri(videoUrl);
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
