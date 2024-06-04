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
            //BindingContext = this;
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
            mediaElement.IsVisible = true;
            if (sender is ImageButton imageButton && imageButton.CommandParameter is string videoUrl)
            {
              
                HighlightButton(imageButton);

               
                mediaElement.Source = new Uri(videoUrl);

                mediaElement.Play();
            }
        }

        private void HighlightButton(ImageButton currentButton)
        {
            // Reset the opacity of the previously highlighted button
            if (_previousButton != null)
            {
                _previousButton.Opacity = 1.0;             }

           
            currentButton.Opacity = 0.5; 
            
           
            _previousButton = currentButton;
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            if (sender is ImageButton button)
            {
                button.BackgroundColor = Colors.DarkGray;
                button.Scale = 0.8;
                Task.Delay(200);
                button.Scale = 1;
            }

        }

       
    }
}