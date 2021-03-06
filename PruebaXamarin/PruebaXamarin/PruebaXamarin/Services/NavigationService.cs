﻿namespace PruebaXamarin.Services
{
    using PruebaXamarin.Pages;
    using System.Threading.Tasks;

    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "LoginPage":
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    break;
                case "ProspectsPage":
                    await App.Current.MainPage.Navigation.PushAsync(new ProspectsPage());
                    break;
                case "EditProspectPage":
                    await App.Current.MainPage.Navigation.PushAsync(new EditProspectPage());
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
