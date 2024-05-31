namespace VidyamAcademy.Views.Startup
{
    public partial class OTPResetPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly string _email;
        private string _pin;


        public OTPResetPage(ApiService apiService, string email)
        {
            InitializeComponent();
            _apiService = apiService;
            _email = email;
            BindingContext = this;
        }

        public string PIN
        {
            get => _pin;
            set
            {
                if (_pin != value)
                {
                    _pin = value;
                    OnPropertyChanged(nameof(PIN));
                }
            }
        }



        private async void OnVerifyOTPClicked(object sender, EventArgs e)
        {
            try
            {
                var otpVerifcationData = new OtpDTO
                {
                    Otp = _pin,
                    Email = _email
                };
                // Verify OTP
                var isSuccess = await _apiService.VerifyForgotOTPAsync(otpVerifcationData);
                foreach (var page in Navigation.NavigationStack)
                {
                    System.Diagnostics.Debug.WriteLine(page.GetType().Name);
                }

                // Find the ForgotPasswordPage in the navigation stack
                var forgotPasswordPage = Navigation.NavigationStack.OfType<ForgotPasswordPage>().FirstOrDefault();
                if (forgotPasswordPage != null)
                {
                    await forgotPasswordPage.HandleOtpVerifiedAsync(_email);
                }


                //if (isSuccess)
                //{

                //    if (Navigation.NavigationStack.LastOrDefault() is ForgotPasswordPage forgotPasswordPage)
                //    {
                //        await forgotPasswordPage.HandleOtpVerifiedAsync(_email);
                //    }

                //    await Navigation.PopAsync();
                //}
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
            // Store the PIN value when PIN entry is completed
            var pinView = (PINView.Maui.PINView)sender;
            PIN = pinView.PINValue;
        }
    }
}
