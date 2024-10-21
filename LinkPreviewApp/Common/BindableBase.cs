using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinkPreviewApp.Common
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChange([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
