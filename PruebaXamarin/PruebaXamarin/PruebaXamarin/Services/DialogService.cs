namespace PruebaXamarin.Services
{
    using System.Threading.Tasks;
       
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
