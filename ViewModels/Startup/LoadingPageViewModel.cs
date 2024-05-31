using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using VidyamAcademy.Models;
using VidyamAcademy.Controls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace VidyamAcademy.ViewModels.Startup
{
    public class LoadingPageViewModel
    {
        private readonly ApiService _apiService;
        public LoadingPageViewModel(ApiService apiService)
        {
            _apiService = apiService;
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get("user_name", "");
            string authToken = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrWhiteSpace(userDetailsStr) || string.IsNullOrWhiteSpace(authToken))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                var userInfo = new UserDetail
                {
                    UserName = userDetailsStr,
                    Id = Preferences.Get("user_id", 0),
                    Role = Preferences.Get("user_role", "")
                };

                App.UserDetails = userInfo;
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl(_apiService);
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
        }
    }
}
