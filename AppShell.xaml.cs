using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using VidyamAcademy.ViewModels;

namespace VidyamAcademy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            var apiService = App.Services.GetService<ApiService>();
            this.BindingContext = new AppShellViewModel(apiService);
        }
    }
}
