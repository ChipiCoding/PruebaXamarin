namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Models;
    using PruebaXamarin.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Windows.Input;

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string nameProperty)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
        }
        #endregion

        #region Properties
        private Login login;
        public Login Login
        {
            set
            {
                if (login != value)
                {
                    login = value;                    
                    PropertyChangedEvent("Login");
                }
            }
            get
            {
                return login;
            }
        }

        private List<Prospect> Prospects { get; set; }
        public ObservableCollection<Prospect> ProspectsCollection { get; set; }

        public Prospect ProspectSeleted { get; set; }
        #endregion

        private DialogService DialogService { get => dialogService; set => dialogService = value; }

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(LoginAutorization); } }
        #endregion

        #region Methods Async
        private async void LoginAutorization()
        {
            if (string.IsNullOrEmpty(login.email))
            {
                await dialogService.ShowMessage("Error", "El correo es obligatorio.");
                return;
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(login.email);
                if (!match.Success)
                    await dialogService.ShowMessage("Error", "El correo no es correcto.");
                else
                {
                    if (string.IsNullOrEmpty(login.password))
                    {
                        await dialogService.ShowMessage("Error", "La clave es obligatoria");
                        return;
                    }
                    else
                        ValidateLogin();
                }
            }
        }

        private async void ValidateLogin()
        {
            Response responseAutentication = await apiService.Autenticate<Response>("/application", "/login", login);
            if (responseAutentication.IsSuccess)
                GetProspects(responseAutentication.Result as Autorization);
            else
                await dialogService.ShowMessage("Error", "Datos no validos");
        }

        private async void GetProspects(Autorization autentication)
        {
            Response response = await apiService.GetProspects("/sch", "/prospects.json", autentication);
            Prospects = new List<Prospect>();
            Prospects = response.Result as List<Prospect>;
            foreach (Prospect item in Prospects)
                ProspectsCollection.Add(item);
            await navigationService.Navigate("ProspectsPage");
        }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            Login = new Login();
#if DEBUG
            Login = new Login { email = "directo@directo.com", password = "directo123" };
#endif
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();
            ProspectsCollection = new ObservableCollection<Prospect>();
        }
        #endregion



    }
}
