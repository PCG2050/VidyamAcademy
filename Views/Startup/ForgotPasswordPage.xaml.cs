using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;


namespace VidyamAcademy.Views.Startup
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly ApiService _apiService;
        private string _otp;

        public ForgotPasswordPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            SendOTPButton.IsVisible = !string.IsNullOrWhiteSpace(EmailEntry.Text);
        }

        private async void OnSendOTPClicked(object sender, EventArgs e)
        {
            try
            {
                await _apiService.ForgotPasswordAsync(EmailEntry.Text);
                await DisplayAlert("Success", "OTP sent successfully!", "OK");
                await Navigation.PushAsync(new OTPResetPage(_apiService, EmailEntry.Text, OnOtpVerified));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void OnOtpVerified(object sender, string email,string otp)
        {
            _otp = otp;
            VerificationStatusLabel.Text = "Verified";
            SendOTPButton.IsEnabled = false;
            EmailEntry.Text = email;
            NewPasswordFrame.IsVisible = true;
            ConfirmPasswordFrame.IsVisible = true;
            ResetPasswordButton.IsVisible = true;
        }

       

        private async void ResetButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(ConfirmNewPasswordEntry.Text))
            {
                await DisplayAlert("Error", "All fields are required", "OK");
                return;
            }

            if (NewPasswordEntry.Text != ConfirmNewPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            try
            {
                await _apiService.VerifyAndSetNewPasswordAsync(EmailEntry.Text, _otp, NewPasswordEntry.Text);
                await DisplayAlert("Success", "Password changed successfully!", "OK");
                await Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to change password: {ex.Message}", "OK");
            }
        }
    }
}
