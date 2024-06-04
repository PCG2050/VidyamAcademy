using VidyamAcademy.Services;
using VidyamAcademy.ViewModels.Startup;

namespace VidyamAcademy.Views.Startup
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;

        public LoginPage(LoginPageViewModel viewModel, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _apiService = apiService;
        }

        private async void ForgotPasswordTapped(object sender, TappedEventArgs e)
        {           
             await Navigation.PushAsync(new ForgotPasswordPage(_apiService));
        }

        private async void OnSignUpLabelTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new SignupPage(_apiService));
        }
    }
}
