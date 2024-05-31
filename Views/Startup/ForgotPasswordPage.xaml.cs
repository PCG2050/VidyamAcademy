namespace VidyamAcademy.Views.Startup
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly ApiService _apiService;

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
            if (string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Please enter your email.", "OK");
                return;
            }

            try
            {
                await _apiService.ForgotPasswordAsync(EmailEntry.Text);
                await DisplayAlert("Success", "OTP sent to your email.", "OK");

                // Navigate to OTP entry page, passing the email
                await Navigation.PushAsync(new OTPResetPage(_apiService, EmailEntry.Text));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to send OTP: {ex.Message}", "OK");
            }
        }

        public async Task HandleOtpVerifiedAsync(string email)
        {
            EmailEntry.Text = email;
            OTPFrame.IsVisible = true;
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
                await _apiService.VerifyAndSetNewPasswordAsync(EmailEntry.Text, OTPEntry.Text, NewPasswordEntry.Text);
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
