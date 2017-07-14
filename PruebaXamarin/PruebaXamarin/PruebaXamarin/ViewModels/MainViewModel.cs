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
    using System.Linq;

    public class MainViewModel
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        #endregion

        public Login login { get; set; }
        public List<Prospect> Prospects { get; set; }
        
        public ObservableCollection<Prospect> ProspectsCollection { get; set; }
        
        #region SIngleton
        private static MainViewModel instance;
        internal static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        public DialogService DialogService { get => dialogService; set => dialogService = value; }


        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }
        #endregion

        #region Methods Async
        private async void Login()
        {
#if DEBUG
            login = new Login { email = "directo@directo.com", password = "directo123" };
#endif
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
            {
                GetProspects(responseAutentication.Result as Autorization);
            }
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
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();            
            login = new Login();
            ProspectsCollection = new ObservableCollection<Prospect>();
        }
        #endregion
    }
}
