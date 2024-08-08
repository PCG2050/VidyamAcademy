using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using VidyamAcademy.Models;

namespace VidyamAcademy.Views
{
    public partial class SubjectPaymentPage : ContentPage
    {
        private readonly Subject _subject;
        private readonly ApiService _apiService;

        public SubjectPaymentPage(Subject subject, ApiService apiService)
        {
            InitializeComponent();
            _subject = subject;
            _apiService = apiService;
            LoadPaymentPage();
        }

        private async void LoadPaymentPage()
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
                //create order result 
                var orderResult = await _apiService.CreateRazorpayOrderAsync(new RazorpayOrderRequestDTO
                {
                    Amount = Convert.ToInt32(_subject.Amount),
                    SubjectId = _subject.SubjectId,
                    UserId = userId,
                    Currency = "INR",
                    MobilePhone = phoneNumber
                });

                if (orderResult == null || String.IsNullOrEmpty(orderResult.OrderId))
                {
                    await DisplayAlert("Error", "Failed to create razorpay order.Please try again", "OK");
                }

                var amountString = (_subject.Amount * 100 ).ToString();               

                var webViewContent = await GetHtmlContent(username, email, phoneNumber, amountString, _subject.Name, orderResult.OrderId);
                PaymentWebView.Source = new HtmlWebViewSource { Html = webViewContent };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load payment details. Please try again. Error:{ex.Message}", "OK");
            }
        }

        private async Task<string> GetHtmlContent(string username, string email, string phoneNumber, string amount, string subjectname, string orderId)
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

                    WeakReferenceMessenger.Default.Send(new PaymnetSuccessfulMessage(_subject));
                    await Application.Current.MainPage.Navigation.PushAsync(new VideosPage(_apiService, _subject));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Payment Failed", "The payment was not successful. Please try again.", "OK");
                }
            });
        }
    }
}
