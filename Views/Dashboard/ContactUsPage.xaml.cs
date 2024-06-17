namespace VidyamAcademy.Views.Dashboard;

public partial class ContactUsPage : ContentPage
{
    private const string InitialUrl = "https://vidyamacademy.com/contact/contact.html";

    public ContactUsPage()
	{
		InitializeComponent();
	}

    private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        if(e.Result != WebNavigationResult.Success)
        {
            DisplayAlert("Error", "Web Page not available", "OK");
            webview.Source = InitialUrl;
        }
    }

    private void WebView_Navigating(object sender, WebNavigatedEventArgs e)
    {
        if (e.Result != WebNavigationResult.Success)
        {
            if (e.Url.StartsWith("mailto:") || e.Url.StartsWith("tel:"))
            {
                // Handle unsupported URLs (display alert etc.)
                DisplayAlert("Unsupported URL", "This URL scheme is not supported in the WebView.", "OK");
            }
            else if (!Uri.IsWellFormedUriString(e.Url, UriKind.Absolute))
            {
                // Handle malformed URLs (display alert etc.)
                DisplayAlert("Invalid URL", "The URL is not valid.", "OK");
            }

            // Revert to initial URL
            webview.Source = InitialUrl;
        }
    }
}