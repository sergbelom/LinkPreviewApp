using LinkPreviewApp.Features.LinkPreview.ViewModels;

namespace LinkPreviewApp.Features.LinkPreview.Views
{
    public partial class LinkPreviewView : ContentView
    {
        public LinkPreviewView()
        {
            InitializeComponent();

            HandlerChanged += OnHandlerChanged;

            //BindingContext = Handler?.MauiContext?.Services.GetService<LinkPreviewViewModel>();
        }

        private void OnHandlerChanged(object? sender, EventArgs e)
        {
            BindingContext = Handler?.MauiContext?.Services.GetService<LinkPreviewViewModel>();
        }
        

    }
}
