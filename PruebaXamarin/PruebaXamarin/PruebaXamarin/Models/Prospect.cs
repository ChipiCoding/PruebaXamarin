using GalaSoft.MvvmLight.Command;
using PruebaXamarin.Services;
using PruebaXamarin.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace PruebaXamarin.Models
{
    public class Prospect
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string SchProspectIdentification { get; set; }
        public string Address { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string StatusCd { get; set; }
        public string ZoneCode { get; set; }
        public string NeighborhoodCode { get; set; }
        public string CityCode { get; set; }
        public string SectionCode { get; set; }
        public string RoleId { get; set; }
        public string AppointableId { get; set; }
        public string RejectedObservation { get; set; }
        public string Observation { get; set; }
        public string Disable { get; set; }
        public string Visited { get; set; }
        public string Callcenter { get; set; }
        public string AcceptSearch { get; set; }
        public string CampaignCode { get; set; }
        public string UserId { get; set; }
        public string ImageFullPath
        {
            get
            {
                switch (StatusCd)
                {
                    //pending
                    case "0":
                        return "https://previews.123rf.com/images/cluckva/cluckva1204/cluckva120400032/13070620-Pending-rubber-stamp-Stock-Vector-pending-icon.jpg";

                    //approved 
                    case "1":
                        return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMU4tL5bxgfN6cQ4d4Lv7onL6-74deXDldYgL6qS8FggXu9HyDjQ";

                    //accepted 
                    case "2":
                        return "https://previews.123rf.com/images/123vector/123vector1407/123vector140700096/29975499-Vector-illustration-of-green-accepted-stamp-icon-Stock-Vector-accepted.jpg";

                    //rejected 
                    case "3":
                        return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQi5oTGA3Cy9TtJa8IE7jFr_FWi1V8HXkbTvUfxCdOiGZVn_AJT";

                    //disabled 
                    case "4":
                        return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQBYBVfzbSz8hYopRDP8PNODyxSqu0OCsUOnZFpgPXbWYg0WpfF";

                    default:
                        return "https://cdn.dribbble.com/users/3765/screenshots/2405263/404.png";
                }
            }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", Name, Surname); }
        }

        public string PersonalInformation
        {
            get { return string.Format("Id: {0} - Tel {1}", SchProspectIdentification, Telephone); }
        }

        public ICommand EditCommand  { get { return new RelayCommand(EditProspect); } }

        private async void EditProspect()
        {
            MainViewModel mainVM = MainViewModel.GetInstance();
            mainVM.ProspectSeleted = this;
            NavigationService navigationService = new NavigationService();
            await navigationService.Navigate("EditProspectPage");
        }
    }
}
