
using System.Reflection;
using System.Threading.Tasks;


namespace VidyamAcademy.Views
{
    public partial class SubjectPaymentPage : ContentPage
    {
        private readonly Subject _subject;

        public SubjectPaymentPage(Subject subject)
        {
            InitializeComponent();
            _subject = subject;

            // Call the method to load the payment page with the course details
            LoadPaymentPage();
        }

        private async Task LoadPaymentPage()
        {
            try
            {
                var username = await SecureStorage.GetAsync("user_username");
                var email = await SecureStorage.GetAsync("user_email");
                var phoneNumber = await SecureStorage.GetAsync("user_phonenumber");

                var webViewContent = await GetHtmlContent(username, email, phoneNumber, _subject.Amount, _subject.Name);

                PaymentWebView.Source = new HtmlWebViewSource { Html = webViewContent };
            }
            catch (Exception ex)
            {
                // Handle potential errors (e.g., missing secure storage values)
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load payment details. Please try again.", "OK");
            }
        }

        private async Task<string> GetHtmlContent(string username, string email, string phoneNumber, string amount, string subjectname)
        {
            if (!decimal.TryParse(amount, out var amountDecimal))
            {
                throw new ArgumentException("Invalid amount format");
            }

            var amountInSubunits = (amountDecimal * 100).ToString("F0");
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("VidyamAcademy.Resources.Raw.payment.html"))
            using (var reader = new StreamReader(stream))
            {
                var htmlContent = await reader.ReadToEndAsync();
                htmlContent = htmlContent.Replace("{{name}}", username)
                                         .Replace("{{email}}", email)
                                         .Replace("{{contact}}", phoneNumber)
                                         .Replace("{{amount}}", amountInSubunits)
                                         .Replace("logo_path", "https://vidyamacademy.com/images/final%20logo_page-0001.jpg")
                                         .Replace("{{payingforName}}", subjectname);
                return htmlContent;
            }
        }
        private async void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.StartsWith("payment-success://"))
            {
                await HandlePaymentResult(true);
            }
            else if (e.Url.StartsWith("payment-failure://"))
            {
                await HandlePaymentResult(false);
            }
        }

        private async Task HandlePaymentResult(bool success)
        {
            await Dispatcher.DispatchAsync(async () =>
            {
                await Navigation.PopModalAsync();
                if (success)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VideosPage(_subject));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Payment Failed", "The payment was not successful. Please try again.", "OK");
                }
            });
        }
    }
}
