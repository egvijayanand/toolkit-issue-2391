using System.Reflection;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        private DisplayOrientation _orientation;

        public MainPage(MainViewModel viewModel, IDeviceDisplay deviceDisplay, IDeviceInfo deviceInfo)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.Interop = SendMessage;
            BindingContext = ViewModel;

            if (deviceInfo.Idiom == DeviceIdiom.Phone || deviceInfo.Idiom == DeviceIdiom.Tablet)
            {
                _orientation = deviceDisplay.MainDisplayInfo.Orientation;
                deviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
            }

            var version = typeof(MauiApp).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            versionLabel.Text = $".NET MAUI ver. {version?[..version.IndexOf('+')]}";
        }

        public MainViewModel ViewModel { get; set; }

        private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation == _orientation)
            {
                return;
            }
            else
            {
                // Orientation changed
                _orientation = e.DisplayInfo.Orientation;

                if (e.DisplayInfo.Orientation == DisplayOrientation.Portrait)
                {
                    container.ColumnDefinitions = [new()];
                    container.RowDefinitions = [new(), new()];
                    Grid.SetRow(hwv, 1);
                }
                else
                {
                    container.ColumnDefinitions = [new(), new()];
                    container.RowDefinitions = [new()];
                    Grid.SetColumn(hwv, 1);
                }
            }
        }

        private void SendMessage()
        {
            hwv.SendRawMessage(ViewModel.Message.Trim());
            ViewModel.Message = string.Empty;
            message.Focus();
        }
    }
}
