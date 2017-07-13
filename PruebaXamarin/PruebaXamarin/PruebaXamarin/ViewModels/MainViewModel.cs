namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Services;
    using System.Text.RegularExpressions;
    using System.Windows.Input;

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
        public DialogService DialogService { get => dialogService; set => dialogService = value; }

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
#if DEBUG
            login = new LoginViewModel { email = "directo@directo.com", password = "directo123" }; 
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
