using System.Runtime.CompilerServices;

namespace LinkPreviewApp.Common
{
    public class BaseViewModel : BindableBase
    {
        protected bool SetValue<T>(ref T privatefiled, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(value, privatefiled))
            {
                return false;
            }

            privatefiled = value;
            OnPropertyChange(propertyName);

            return true;
        }
    }
}
