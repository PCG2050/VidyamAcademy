using System;
using Microsoft.Maui.Controls;


namespace VidyamAcademy.Views.Startup
{
    public partial class OTPResetPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly string _email;
        private readonly Action<object, string, string> _onOtpVerified;

        public OTPResetPage(ApiService apiService, string email, Action<object, string, string> onOtpVerified)
        {
            InitializeComponent();
            _apiService = apiService;
            _email = email;
            _onOtpVerified = onOtpVerified;
            BindingContext = this;
        }

        public string PIN { get; set; }

        private async void OnVerifyOTPClicked(object sender, EventArgs e)
        {
            try
            {
                var otpVerificationData = new OtpDTO
                {
                    Otp = PIN,
                    Email = _email
                };

                var isSuccess = await _apiService.VerifyForgotOTPAsync(otpVerificationData);

                if (isSuccess)
                {
                    // Trigger the OTP verified callback
                    _onOtpVerified?.Invoke(this, _email, PIN);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to verify OTP. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnPINEntryCompleted(object sender, EventArgs e)
        {
            var pinView = (PINView.Maui.PINView)sender;
            PIN = pinView.PINValue;
        }
    }
}
