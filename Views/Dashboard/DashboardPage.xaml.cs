
using System.Timers;


namespace VidyamAcademy.Views.Dashboard
{
    public partial class DashboardPage : ContentPage
    {
        private System.Timers.Timer _carouselTimer;

        public DashboardPage(DashboardPageViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            StartCarousel();
        }

        private void StartCarousel()
        {
            _carouselTimer = new System.Timers.Timer(2000); // Set timer to 2 seconds
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

        private void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
                {
                    Navigation.PushAsync(new SubjectsPage(selectedCourse.Subjects));
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in OnCourseSelected: {ex.Message}");
            }
            finally
            {
                // Reset the selection
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }

    public class ImageItem
    {
        public string ImageSource { get; set; }
    }
}
