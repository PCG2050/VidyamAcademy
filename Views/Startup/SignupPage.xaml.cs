using System;
using System.Threading.Tasks;

using Newtonsoft.Json;
using VidyamAcademy.Models;


namespace VidyamAcademy.Views.Startup
{
    public partial class SignupPage : ContentPage
    {
        private readonly ApiService _apiService;
        public SignupPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnSendOTPClicked(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumber.Text))
            {
                // Display an error message if any field is empty
                await DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            // Check if passwords match
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                // Display an error message if passwords do not match
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            // Call the method to send OTP and sign up
            await SendOTPAndSignUp();
        }

        private async Task SendOTPAndSignUp()
        {
            // Prepare the sign-up data
            var signUpData = new SignUpDTO
            {
                UserName = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                Email = EmailEntry.Text,
                PhoneNumber = PhoneNumber.Text
            };

            try
            {
               
                // Send the sign-up data to the API
                var isSuccess = await _apiService.SignUpAsync(signUpData); 
                //var isSuccess = await ApiService.GetInstance(ApiUrls.BaseUrl).SignUpAsync(signUpData);

                if (isSuccess)
                {
                    // Display a success message
                    await DisplayAlert("Success", "OTP sent to your Email Address!", "OK");

                    await Navigation.PushAsync(new OTPEntryPage(_apiService,EmailEntry.Text));
                }
                else
                {
                    // Display an error message if sign-up fails
                    await DisplayAlert("Error", "Failed to create account. Please try again later.", "OK");
                }
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("unique constraint"))
            {
                // Handle the case where the email is not unique
                await DisplayAlert("Error", "Email already exists. Please use a different email address.", "OK");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        
    }
}
