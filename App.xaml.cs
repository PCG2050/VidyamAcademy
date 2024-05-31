using Microsoft.Maui.Platform;
using VidyamAcademy.Handlers;
using VidyamAcademy.Models;

namespace VidyamAcademy
{
    public partial class App : Application
    {
        
            public static UserDetail UserDetails;
            public App()
            {
                InitializeComponent();
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
