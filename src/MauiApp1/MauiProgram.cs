using Microsoft.Extensions.Logging;

namespace MauiApp1
{
    public static partial class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseMauiCommunityToolkit()
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                       fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                   });

            builder.Services.AddSingleton(DeviceDisplay.Current);
            builder.Services.AddSingleton(DeviceInfo.Current);
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            // Caution: Recommended to enable Developer Tools only for development!!!
            builder.Services.AddHybridWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
