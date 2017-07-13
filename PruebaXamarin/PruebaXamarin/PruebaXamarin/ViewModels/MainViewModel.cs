using GalaSoft.MvvmLight.Command;
using PruebaXamarin.Classes;
using PruebaXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PruebaXamarin.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;

        public LoginViewModel login { get; set; }

        private static MainViewModel instance;
        internal static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        private NavigationService navigationService;
        //private LoginViewModel login;

        public DialogService DialogService { get => dialogService; set => dialogService = value; }

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            //if (login == null)
            //{
                login = new LoginViewModel { email = "directo@directo.com", password = "directo123" }; 
            //}            
            if (string.IsNullOrEmpty(login.email))
            {
                await dialogService.ShowMessage("Error", "El correo es obligatorio.");
                return;
            }

            if (string.IsNullOrEmpty(login.password))
            {
                await dialogService.ShowMessage("Error", "La clave es obligatoria");
                return;
            }

            ValidateLogin();
        }

        private async void ValidateLogin()
        {
            //var response = await apiService.Autenticate<Response>("/application", "/login", login);
            var response1 = await apiService.Get<Response>("http://directotesting.igapps.co/", "/application", "/login", login);
        }
        #endregion
        public MainViewModel()
        {
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();
            login = new LoginViewModel();
        }
    }
}
