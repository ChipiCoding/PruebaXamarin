namespace PruebaXamarin.Models
{
    public class Prospect
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string telephone { get; set; }
        public string schProspectIdentification { get; set; }
        public string address { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string statusCd { get; set; }
        public string zoneCode { get; set; }
        public string neighborhoodCode { get; set; }
        public string cityCode { get; set; }
        public string sectionCode { get; set; }
        public string roleId { get; set; }
        public string appointableId { get; set; }
        public string rejectedObservation { get; set; }
        public string observation { get; set; }
        public string disable { get; set; }
        public string visited { get; set; }
        public string callcenter { get; set; }
        public string acceptSearch { get; set; }
        public string campaignCode { get; set; }
        public string userId { get; set; }
        public string ImageFullPath
        {
            get
            {
                switch (statusCd)
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


    }
}
