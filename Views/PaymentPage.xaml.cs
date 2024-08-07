using System.Reflection;
using System.Threading.Tasks;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels;
using VidyamAcademy.Models;

namespace VidyamAcademy.Views
{
    public partial class PaymentPage : ContentPage
    {
        private readonly Course _course;
        private readonly ApiService _apiService;

        public PaymentPage(Course course, ApiService apiService)
        {
            InitializeComponent();
            _course = course;
            _apiService = apiService;

            // Call the method to load the payment page with the course details
            LoadPaymentPage();
        }

        private async Task LoadPaymentPage()
        {
            try
            {
                var username = await SecureStorage.GetAsync("user_name");
                var email = await SecureStorage.GetAsync("user_email");
                var phoneNumber = await SecureStorage.GetAsync("user_phonenumber");
                var userIdString = await SecureStorage.GetAsync("user_id");

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(userIdString))
                {
                    await DisplayAlert("Error", "Failed to retrieve user details. Please try again.", "OK");
                    return;
                }

                int userId = Convert.ToInt32(userIdString);

                // Create Razorpay order
                var orderResult = await _apiService.CreateRazorpayOrderAsync(new RazorpayOrderRequestDTO
                {
                    Amount = Convert.ToInt64(_course.Amount), 
                    CourseId = _course.CourseId,
                    UserId = userId,
                    Currency = "INR",
                    MobilePhone = phoneNumber
                });

                if (orderResult == null || string.IsNullOrEmpty(orderResult.OrderId))
                {
                    await DisplayAlert("Error", "Failed to create Razorpay order. Please try again.", "OK");
                    return;
                }

                var amountString = (_course.Amount * 100).ToString(); // Amount in subunits (paise)

                // Generate HTML content
                var webViewContent = await GetHtmlContent(username, email, phoneNumber, amountString, _course.Name, orderResult.OrderId);

                // Set WebView source
                PaymentWebView.Source = new HtmlWebViewSource { Html = webViewContent };
            }
            catch (Exception ex)
            {
                // Handle potential errors (e.g., missing secure storage values)
                await DisplayAlert("Error", $"Failed to load payment details. Please try again. Error: {ex.Message}", "OK");
            }
        }


        private async Task<string> GetHtmlContent(string username, string email, string phoneNumber, string amount, string coursename, string orderId)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("VidyamAcademy.Resources.Raw.payment.html"))
            using (var reader = new StreamReader(stream))
            {
                var htmlContent = await reader.ReadToEndAsync();
                htmlContent = htmlContent.Replace("{{name}}", username)
                                         .Replace("{{email}}", email)
                                         .Replace("{{contact}}", phoneNumber)
                                         .Replace("{{amount}}", amount)
                                         .Replace("{{orderId}}", orderId)
                                         .Replace("logo_path", "https://vidyamacademy.com/images/final%20logo_page-0001.jpg")
                                         .Replace("{{payingforName}}", coursename);
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
                    var apiService = DependencyService.Get<ApiService>();
                    await Application.Current.MainPage.Navigation.PushAsync(new SubjectsPage(apiService, _course.CourseId));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Payment Failed", "The payment was not successful. Please try again.", "OK");
                }
            });
        }
    }
}
