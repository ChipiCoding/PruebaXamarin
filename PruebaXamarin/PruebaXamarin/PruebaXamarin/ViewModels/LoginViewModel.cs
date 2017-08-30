namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using HockeyApp;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Models;
    using PruebaXamarin.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Xamarin.Auth;

    public class LoginViewModel : BaseViewModel
    {
        public Login Login { get; set; }

        public ProspectViewModel Prospect { get; set; }


        public ICommand LoginCommand { get { return new RelayCommand(LoginAutorization); } }

        private async void LoginAutorization()
        {
            if (string.IsNullOrEmpty(Login.email))
            {
                await dialogService.ShowMessage("Error", "Email is required.");
                return;
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(Login.email);
                if (!match.Success)
                    await dialogService.ShowMessage("Error", "Mail format is not valid.");
                else
                {
                    if (string.IsNullOrEmpty(Login.password))
                    {
                        await dialogService.ShowMessage("Error", "The password is required.");
                        return;
                    }
                    else
                        ValidateLogin();
                }
            }
        }

        private async void ValidateLogin()
        {
            Classes.Response responseAutentication = await apiService.Autenticate<Classes.Response>("/application", "/login", Login);
            if (responseAutentication.IsSuccess)
            {
                MetricsManager.TrackEvent("Lógin exitoso");

                Prospect.GetProspects(responseAutentication.Result as Autorization);
                if (SaveData)
                    SaveCredentials(Login);
            }
            else
                await dialogService.ShowMessage("Error", "Invalid data.");
        }

        public bool SaveData { get; set; }

        public string UserEmail
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService("PruebaXAmarin").FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public string UserPassword
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService("PruebaXAmarin").FirstOrDefault();
                return (account != null) ? account.Properties["Password"] : null;
            }
        }

        public void SaveCredentials(Login login)
        {
            Account account = new Account
            {
                Username = login.email
            };
            account.Properties.Add("Password", login.password);
            AccountStore.Create().Save(account, "PruebaXAmarin");
        }

        public LoginViewModel()
        {
            Login = new Login();
            Prospect = new ProspectViewModel();
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();
        }
    }
}
