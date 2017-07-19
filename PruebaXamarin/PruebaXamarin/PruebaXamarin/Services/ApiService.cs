namespace PruebaXamarin.Services
{
    using Newtonsoft.Json;
    using PruebaXamarin.Classes;
    using PruebaXamarin.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class ApiService
    {
        public string urlBase = "http://directotesting.igapps.co";
        public async Task<Response> Autenticate<T>(string servicePrefix, string controller, Login login)
        {
            try
            {                
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlBase);                    
                    string url = string.Format("{0}{1}?email={2}&password={3}", servicePrefix, controller, login.email, login.password);
                    HttpResponseMessage responseService = await client.GetAsync(url);

                    if (!responseService.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = responseService.StatusCode.ToString(),
                        };
                    }
                    string result = await responseService.Content.ReadAsStringAsync();
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = JsonConvert.DeserializeObject<Autorization>(result),
                    }; 
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response>GetProspects(string servicePrefix, string file, Autorization login)
        {
            try
            {                
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseService;
                    if (!string.IsNullOrWhiteSpace(login.authToken))
                    {                      
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("token", login.authToken);
                    }
                    responseService = await client.GetAsync(string.Format("{0}{1}{2}", urlBase, servicePrefix, file));
                    string result = responseService.Content.ReadAsStringAsync().Result;
                    if (!responseService.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = responseService.StatusCode.ToString(),
                        };
                    }
                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Ok",
                        Result = JsonConvert.DeserializeObject<List<Prospect>>(result),
                    };
                }
            }
            catch (Exception ex)
            {                
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
