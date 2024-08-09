namespace VidyamAcademy.Views.Dashboard
{
    public partial class ContactUsPage : ContentPage
    {
        private const string InitialUrl = "https://vidyamacademy.com/contact/contact.html";

        public ContactUsPage()
        {
            InitializeComponent();
        }

        private async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            try
            {
                if (e.Result != WebNavigationResult.Success)
                {
                    await DisplayAlert("Error", "Web Page not available", "OK");
                    webview.Source = InitialUrl;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }
        }

        private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            try
            {
                if (e.Url.StartsWith("mailto:") || e.Url.StartsWith("tel:"))
                {
                    e.Cancel = true; // Cancel the navigation
                    await DisplayAlert("Unsupported URL", "This URL scheme is not supported in the WebView.", "OK");
                }
                else if (!Uri.IsWellFormedUriString(e.Url, UriKind.Absolute))
                {
                    e.Cancel = true; // Cancel the navigation
                    await DisplayAlert("Invalid URL", "The URL is not valid.", "OK");
                    webview.Source = InitialUrl;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error occurred: {ex.Message}", "OK");
            }

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            webview.Source = InitialUrl;
        }


    }
}
