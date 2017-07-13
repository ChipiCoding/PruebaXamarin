namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;

    public class LoginViewModel : Login, INotifyPropertyChanged
    {
        #region Attributes
        private DialogService dialogService;
        private ApiService apiService;
        private NavigationService navigationService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        //public LoginViewModel login { get; set; }
        #endregion

        public LoginViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true;
        }
        //public ICommand NewLoginCommand { get { return new RelayCommand(NewLogin); } }

        //private async void NewLogin()
        //{
        //    //if (string.IsNullOrEmpty(lo  EmailAddress))
        //    //{
        //    //    await dialogService.ShowMessage("Error", "You must enter a first name");
        //    //    return;
        //    //}

        //    if (string.IsNullOrEmpty("sdf"))
        //    {
        //        await dialogService.ShowMessage("Error", "You must enter a last name");
        //        return;
        //    }
        //    Validate();
        //}

        //public ICommand LoginCommand { get { return new RelayCommand(LoginComm); } }

        //private async void LoginComm()
        //{
        //    if (login == null)
        //    {
        //        login = new LoginViewModel { email = "directo@directo.com", password = "directo123" };
        //    }
        //    if (string.IsNullOrEmpty(login.email))
        //    {
        //        await dialogService.ShowMessage("Error", "El correo es obligatorio.");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(login.password))
        //    {
        //        await dialogService.ShowMessage("Error", "La clave es obligatoria");
        //        return;
        //    }

        //    //ValidateLogin();
        //}
    }
}
