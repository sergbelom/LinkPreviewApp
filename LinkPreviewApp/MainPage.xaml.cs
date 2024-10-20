namespace LinkPreviewApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            HandlerChanged += OnHandlerChanged;
        }

        private void OnHandlerChanged(object? sender, EventArgs e)
        {
            BindingContext = Handler?.MauiContext?.Services.GetService<MainPageViewModel>();
        }
    }
}
