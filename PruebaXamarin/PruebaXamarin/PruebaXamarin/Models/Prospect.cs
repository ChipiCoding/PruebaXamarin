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
    }
}
