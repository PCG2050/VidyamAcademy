

using VidyamAcademy;

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
            //Views
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddTransient<OTPEntryPage>();
            builder.Services.AddTransient<OTPResetPage>();
            builder.Services.AddTransient<SignupPage>();
            builder.Services.AddTransient<ForgotPasswordPage>();



            //ViewModel
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<DashboardPageViewModel>();
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddTransient<VideosPageViewModel>();

            // Services
            builder.Services.AddSingleton<ApiService>(sp =>
            {
                var httpClient = new HttpClient();
                var baseUrl = "https://debugvidyam.azurewebsites.net/api/";
                var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vidyamblob;AccountKey=63229QeKJbgOHpQyV6Mf4m+kCURgq+SRK/ppz4MI6b5vNJcTW1xGdwPZm5Tk+3Y/F4cJZGHIgRiM+AStmcCQ6g==;EndpointSuffix=core.windows.net";

                return new ApiService(httpClient, baseUrl, blobStorageConnectionString);
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
