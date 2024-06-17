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

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result != WebNavigationResult.Success)
            {
                DisplayAlert("Error", "Webpage not available. Reloading...", "OK");
                webView.Source = InitialUrl;
            }
        }
    }
}
