namespace VidyamAcademy.Views.Startup
{
    public partial class OTPEntryPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly string _email;
        private string _pin;


        public OTPEntryPage(ApiService apiService, string email)
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
                var isSuccess = await _apiService.VerifySignUpOTPAsync(otpVerifcationData);

                if (isSuccess)
                {
                    
                    await DisplayAlert("Success", "Account created and verified successfully!", "OK");

                    // Navigate back to the login page
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
            // Store the PIN value when PIN entry is completed
            var pinView = (PINView.Maui.PINView)sender;
            PIN = pinView.PINValue;
        }
    }
}
