namespace PruebaXamarin.ViewModels
{
    using Services;
    using System.ComponentModel;

    public class BaseViewModel : INotifyPropertyChanged
    {

        #region Attributes
        public ApiService apiService;
        public DialogService dialogService;
        public NavigationService navigationService;
        #endregion

        #region Properties
        public DialogService DialogService { get => dialogService; set => dialogService = value; } 
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void PropertyChangedEvent(string nameProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProperty));
        }
        #endregion
    }
}
