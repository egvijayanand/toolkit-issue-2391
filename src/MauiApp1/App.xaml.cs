namespace MauiApp1
{
    public partial class App : Application
    {
        private readonly IServiceProvider _services;

        public App(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;

            UserAppTheme = PlatformAppTheme;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(_services.GetRequiredService<MainPage>());
            window.Title = "MauiApp1";
            return window;
        }
    }
}
