namespace PruebaXamarin.Services
{
    using Newtonsoft.Json;
    using PruebaXamarin.Classes;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public class ApiService
    {
        public async Task<Response> Get<T>(string urlBase, string servicePrefix, string controller, Login login)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //"http://directotesting.igapps.co/application/login?email=directo@directo.com&password=directo123";
                var url = string.Format("{0}{1}?email={2}&password={3}", servicePrefix, controller, login.email, login.password);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
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

        public async Task<Response> Post<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
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

        public async Task<Response> Put<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, model.GetHashCode());
                var response = await client.PutAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record updated OK",
                    Result = newRecord,
                };
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

        public async Task<Response> Delete<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, model.GetHashCode());
                var response = await client.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record deleted OK",
                };
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

        public async Task<Response> Autenticate<T>(string servicePrefix, string controller, Login model)
        {
            try
            {
                //var client = new HttpClient();
                //client.BaseAddress = new Uri("http://directotesting.igapps.co/");
                //var url = string.Format("{0}{1}", servicePrefix, controller);
                //var formcontent = new FormUrlEncodedContent(new[]
                //{
                //        new KeyValuePair<string,string>("email_id", model,em),
                //        new KeyValuePair<string, string>("password","shah")
                //    });
                //var response = await client.GetAsync(url);

                //if (!response.IsSuccessStatusCode)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = response.StatusCode.ToString(),
                //    };
                //}

                //var result = await response.Content.ReadAsStringAsync();
                //var list = JsonConvert.DeserializeObject<List<T>>(result);
                //return new Response
                //{
                //    IsSuccess = true,
                //    Message = "Ok",
                //    Result = list,
                //};

                //using (var c = new HttpClient())
                //{
                //    var client = new System.Net.Http.HttpClient();
                //    var jsonRequest = new { email = model.email, password = model.password };

                //    var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
                //    HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/login");

                //    var response = await client.PostAsync(new Uri("http://directotesting.igapps.co/app"), content);

                //    if (response.IsSuccessStatusCode)
                //    {
                //        var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        
                //    }
                //}


                using (var cl = new HttpClient())
                {
                    var formcontent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string,string>("email", model.email),
                        new KeyValuePair<string, string>("password", model.password)
                });


                    var request = await cl.PostAsync("http://directotesting.igapps.co/application/login", formcontent);

                    request.EnsureSuccessStatusCode();

                    var response = await request.Content.ReadAsStringAsync();

                    Response res = JsonConvert.DeserializeObject<Response>(response);

                    return res;

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
