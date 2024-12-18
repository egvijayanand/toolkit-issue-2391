namespace MauiApp1.ViewModels
{
    public partial class MainViewModel(IDispatcher dispatcher) : BaseViewModel("Home")
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSendMessage))]
        public partial string Message { get; set; } = string.Empty;
    
        [ObservableProperty]    
        public partial string Messages { get; set; } = string.Empty;

        public bool CanSendMessage => !string.IsNullOrWhiteSpace(Message);

        public Action? Interop { get; set; }

        [RelayCommand(CanExecute = nameof(CanSendMessage))]
        private void SendMessage() => Interop?.Invoke();

        [RelayCommand]
        private void ShowMessage(string message)
            => dispatcher.Dispatch(() => Messages += message + Environment.NewLine);
    }
}
