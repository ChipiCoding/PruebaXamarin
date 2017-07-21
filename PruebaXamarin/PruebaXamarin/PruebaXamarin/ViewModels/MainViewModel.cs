namespace PruebaXamarin.ViewModels
{
    using PruebaXamarin.Services;

    public class MainViewModel : BaseViewModel
    {
        private LoginViewModel loginVM;
        public LoginViewModel LoginVM
        {
            set
            {
                if (loginVM != value)
                {
                    loginVM = value;
                    PropertyChangedEvent("LoginVM");
                }
            }
            get
            {
                return loginVM;
            }
        }

        public ProspectViewModel ProspectVM { get; set; }

        #region Properties

        //public ProspectViewModel ProspectSeleted { get; set; }
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
        

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            LoginVM = new LoginViewModel();
            ProspectVM = new ProspectViewModel();
            if (!string.IsNullOrEmpty(LoginVM.UserEmail) && !string.IsNullOrEmpty(LoginVM.UserPassword))
            {
                LoginVM.Login.email = LoginVM.UserEmail;
                LoginVM.Login.password = LoginVM.UserPassword;
            }            
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();            
        }
        #endregion
    }
}
