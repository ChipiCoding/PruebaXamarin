namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Models;
    using PruebaXamarin.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Xamarin.Auth;

    public class MainViewModel : BaseViewModel
    {
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

        private bool isLogging;

        public bool IsLogging
        {
            get { return isLogging; }
            set
            {
                if (isLogging != value)
                {
                    isLogging = value;
                    PropertyChangedEvent("IsLogging");
                };
            }
        }


        #region Properties
        private List<Prospect> Prospects { get; set; }
        public ObservableCollection<Prospect> ProspectsCollection { get; set; }

        public Prospect ProspectSeleted { get; set; }
        #endregion

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
                await dialogService.ShowMessage("Error", "Email is required.");
                return;
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(login.email);
                if (!match.Success)
                    await dialogService.ShowMessage("Error", "Mail format is not valid.");
                else
                {
                    if (string.IsNullOrEmpty(login.password))
                    {
                        await dialogService.ShowMessage("Error", "The password is required.");
                        return;
                    }
                    else
                    {
                        IsLogging = true;
                        ValidateLogin();
                        IsLogging = false;
                    }
                }
            }
        }

        private async void ValidateLogin()
        {
            Classes.Response responseAutentication = await apiService.Autenticate<Classes.Response>("/application", "/login", login);
            if (responseAutentication.IsSuccess)
            {
                GetProspects(responseAutentication.Result as Autorization);
                if (login.SaveData)
                    login.SaveCredentials(login);
            }
            else
                await dialogService.ShowMessage("Error", "Invalid data.");
        }

        private async void GetProspects(Autorization autorization)
        {
            Classes.Response response = await apiService.GetProspects("/sch", "/prospects.json", autorization);
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
            if (!string.IsNullOrEmpty(Login.UserEmail) && !string.IsNullOrEmpty(Login.UserPassword))
            {
                Login.email = Login.UserEmail;
                Login.password = Login.UserPassword;
            }
            IsLogging = false;
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();
            ProspectsCollection = new ObservableCollection<Prospect>();
        }
        #endregion
    }
}
