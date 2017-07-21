namespace PruebaXamarin.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Models;
    using PruebaXamarin.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class ProspectViewModel : BaseViewModel
    {
        private MainViewModel main;
        public ICommand EditCommand { get { return new RelayCommand(EditProspect); } }

        public Prospect Prospect { get; set; }

        private List<Prospect> Prospects { get; set; }
        public ObservableCollection<Prospect> ProspectsCollection { get; set; }

        //public ProspectViewModel ProspectSeleted { get; set; }

        private Prospect prospectSeleted;

        public Prospect ProspectSeleted
        {
            get { return prospectSeleted; }
            set
            {
                if (prospectSeleted != value)
                {
                    prospectSeleted = value;
                    PropertyChangedEvent("ProspectSeleted");
                }
            }
        }


        private async void EditProspect()
        {
            //mainVM = MainViewModel.GetInstance();            
            NavigationService navigationService = new NavigationService();
            await navigationService.Navigate("EditProspectPage");
        }

        public async void GetProspects(Autorization autorization)
        {
            Response response = await apiService.GetProspects("/sch", "/prospects.json", autorization);
            Prospects = new List<Prospect>();
            Prospects = response.Result as List<Prospect>;
            foreach (Prospect item in Prospects)
                ProspectsCollection.Add(item);
            
            main.ProspectVM.ProspectsCollection = ProspectsCollection;
            await navigationService.Navigate("ProspectsPage");
        }

        public ProspectViewModel()
        {
            main = MainViewModel.GetInstance();
            apiService = new ApiService();
            DialogService = new DialogService();
            navigationService = new NavigationService();
            prospectSeleted = new Prospect();
            ProspectsCollection = new ObservableCollection<Prospect>();
        }
    }
}
