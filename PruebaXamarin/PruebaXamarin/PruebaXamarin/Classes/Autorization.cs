namespace PruebaXamarin.Classes
{
   public  class Autorization
    {
        //{"success":true,"authToken":"9FQn5gzKA9k2LWyBMZtM","email":"directo@directo.com","zone":null}

        public bool success { get; set; }
        public string authToken { get; set; }
        public string email { get; set; }
        public string zone { get; set; }
    }
}
