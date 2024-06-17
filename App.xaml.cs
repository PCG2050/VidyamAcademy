using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Platform;
using System.Globalization;
using VidyamAcademy.Handlers;
using VidyamAcademy.Models;

namespace VidyamAcademy
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        public static UserDetail UserDetails;
            public App(IServiceProvider serviceProvider)
        {
                InitializeComponent();

               Services = serviceProvider;


            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            // Borderless Entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
                {
                    if (view is BorderlessEntry)
                    {
#if __ANDROID__
                        handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                    handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
                    }
                });
                MainPage = new AppShell();
            }
        
    }
}





