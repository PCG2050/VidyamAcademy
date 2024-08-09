using Microsoft.Maui.Controls;

namespace VidyamAcademy.Views.Dashboard
{
    public partial class AboutUsPage : ContentPage
    {
        private const string InitialUrl = "https://vidyamacademy.com/about-us/about-us.html";

        public AboutUsPage()
        {
            InitializeComponent();
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            loadingIndicator.IsVisible = false;
            loadingIndicator.IsRunning = false;

            try
            {
                if (e.Result != WebNavigationResult.Success)
                {
                    await DisplayAlert("Error", "Webpage not available.", "OK");
                    webView.Source = InitialUrl;
                    retryButton.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
                retryButton.IsVisible = true;
            }
        }

        private void RetryButton_Clicked(object sender, EventArgs e)
        {
            retryButton.IsVisible = false;
            loadingIndicator.IsVisible = true;
            loadingIndicator.IsRunning = true;
            webView.Source = InitialUrl;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            webView.Source = InitialUrl; // Load the initial URL when the page disappears
        }
    }
}
