using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace VidyamAcademy.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public LoginPageViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        public async Task Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var loginData = new LoginDTO { Email = Email, Password = Password };

                try
                {
                    var loginResponse = await _apiService.LoginAsync(loginData);

                    if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                    {
                        await SaveLoginDataAsync(loginResponse);

                        await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                    }
                    else
                    {
                        // Show error message
                        await Application.Current.MainPage.DisplayAlert("Login Failed", "Please check your credentials.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Show error message
                    await Application.Current.MainPage.DisplayAlert("Error", $"Login error: {ex.Message}", "OK");
                }
            }
            else
            {
                // Show validation message
                await Application.Current.MainPage.DisplayAlert("Validation Error", "Email and Password cannot be empty.", "OK");
            }
        }

        private async Task SaveLoginDataAsync(LoginResponseDTO loginResponse)
        {
            try
            {
                await SecureStorage.SetAsync("auth_token", loginResponse.Token);
                await SecureStorage.SetAsync("refresh_token", loginResponse.RefreshToken);

                Preferences.Set("user_id", loginResponse.UserDetail.Id);
                Preferences.Set("user_name", loginResponse.UserDetail.UserName);
                Preferences.Set("user_role", loginResponse.UserDetail.Role);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error saving login data: {ex.Message}", "OK");
            }
        }
    }
}
