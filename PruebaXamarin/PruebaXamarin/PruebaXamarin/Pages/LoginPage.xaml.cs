namespace PruebaXamarin.Pages
{
    using PruebaXamarin.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //MainViewModel mainViewModel = MainViewModel.GetInstance();
            //base.Appearing += (object sender, EventArgs e) =>
            //{
            //    mainViewModel.LoginCommand.Execute(this);
            //};
        }
    }
}
