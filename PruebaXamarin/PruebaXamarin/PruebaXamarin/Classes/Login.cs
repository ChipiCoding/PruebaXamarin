namespace PruebaXamarin.Classes
{
    using System.Linq;
    using Xamarin.Auth;

    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }

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
    }
}
