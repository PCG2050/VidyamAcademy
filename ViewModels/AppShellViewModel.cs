using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

namespace VidyamAcademy.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public AppShellViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        async Task SignOutAsync()
        {
           
            ClearUserData();

            
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

            _apiService.ClearAuthHeader();


            //ClearViewModelsData();
        }

        private void ClearUserData()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }

            SecureStorage.RemoveAll();
        }

        //private void ClearViewModelsData()
        //{
        // 
        //    var ViewModel = ServiceLocator.Get<SomeViewModel>();
        //    ViewModel.ClearData();
        //}
    }
}
