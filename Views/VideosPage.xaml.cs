using Microsoft.Maui.ApplicationModel;
namespace VidyamAcademy.Views
{
    public partial class VideosPage : ContentPage
    {
        public List<Video> Videos { get; private set; }
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
            if (BindingContext is VideosPageViewModel viewModel)
            {
                videosCollectionView.ItemsSource = viewModel.Videos;
            }
          

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
                var video = (Video)imageButton.BindingContext; 
                if (video.IsFree || !string.IsNullOrEmpty(video.SasToken))
                {
                    
                    HighlightButton(imageButton);

                    
                    mediaElement.IsVisible = true;
                    mediaElement.Source = new Uri(video.EffectiveUrl);
                    mediaElement.Play();
                    
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
    }
}
