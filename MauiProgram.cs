using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels;
using VidyamAcademy.Views;

namespace VidyamAcademy
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register Views
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddTransient<OTPEntryPage>();
            builder.Services.AddTransient<OTPResetPage>();
            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<ForgotPasswordPage>();

            // Register ViewModels
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<DashboardPageViewModel>();
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddTransient<VideosPageViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();

            // Register Services
            builder.Services.AddSingleton<ApiService>(sp =>
            {
                var httpClient = new HttpClient();
                var baseUrl = "https://debugvidyam.azurewebsites.net/api/";
                var blobStorageConnectionString = "YourBlobStorageConnectionString";

                return new ApiService(httpClient, baseUrl, blobStorageConnectionString);
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();

            return mauiApp;
        }
    }
}
