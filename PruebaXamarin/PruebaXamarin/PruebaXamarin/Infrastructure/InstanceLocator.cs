using PruebaXamarin.Pages;
using PruebaXamarin.ViewModels;
using Xamarin.Forms;

namespace PruebaXamarin.Infrastructure
{
    
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
