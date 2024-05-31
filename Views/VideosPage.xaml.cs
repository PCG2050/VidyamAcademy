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
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            videosCollectionView.ItemsSource = Videos;
        }

        private void OnThumbnailTapped(object sender, EventArgs e)
        {
            mediaElement.IsVisible = true;
            if (sender is ImageButton imageButton && imageButton.CommandParameter is string videoUrl)
            {
                // Update highlighting
                HighlightButton(imageButton);

                // Set the MediaElement source to the tapped videoUrl
                mediaElement.Source = new Uri(videoUrl);

                // Play the video
                mediaElement.Play();
            }
        }

        private void HighlightButton(ImageButton currentButton)
        {
            // Reset the opacity of the previously highlighted button
            if (_previousButton != null)
            {
                _previousButton.Opacity = 1.0; // Reset opacity to fully visible
            }

            // Highlight the current button by reducing its opacity
            currentButton.Opacity = 0.5; // Adjust opacity as needed

            _previousButton = currentButton;
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                button.BackgroundColor = Colors.DarkGray;
                button.Scale = 0.8;
            }
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                button.BackgroundColor = Colors.LightGray;
                button.Scale = 1.0;
            }
        }
    }
}