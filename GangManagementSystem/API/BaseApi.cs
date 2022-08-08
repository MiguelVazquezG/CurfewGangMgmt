using Newtonsoft.Json;
using GangManagementSystem.Properties;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GangManagementSystem.API
{
    public class BaseApi
    {
        private static string _svcUrl;
        private static HttpClient httpClient = new HttpClient();
        private static Settings settings = new Settings();

        protected static string LocalApplication = "GangManagementSystem";

        static BaseApi()
        {
            switch (settings.AppLevel)
            {
                case "L":
                    _svcUrl = settings.svcGangManagementQA;
                    break;
                case "D":
                    _svcUrl = settings.svcGangManagementDEV;
                    break;
                case "Q":
                    _svcUrl = settings.svcGangManagementQA;
                    break;
                case "P":
                    _svcUrl = settings.svcGangManagementPROD;
                    break;
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected static T GetEndpoint<T>(string action)
        {
            SetHeaders();
            return Endpoint<T>(action, url => httpClient.GetAsync(url));
        }

        protected static T PostObject<T, R>(string action, R item)
        {
            SetHeaders();
            return Endpoint<T>(action, url => httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")));
        }
        protected static T PostEndpoint<T>(string action, T content)
        {
            SetHeaders();
            return PostEndpoint<T, T>(action, content);
        }

        protected static R PostEndpoint<T, R>(string action, T content)
        {
            return Endpoint<R>(action, url => httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")));
        }

        private static R Endpoint<R>(string action, Func<Uri, Task<HttpResponseMessage>> apiCall)
        {
            try
            {
                var url = new Uri(_svcUrl + "/" + Uri.EscapeUriString(action));

                var response = apiCall(url).Result;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = response.Content.ReadAsAsync<JsonResponse>().Result;
                        return json.GetResult<R>();
                    }
                    else
                        return default(R);
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException.GetType() == typeof(HttpRequestException))
                    ErrorApi.SendErrorEmail(ex);
            }
            catch (System.Exception ex)
            {
                // Service has already logged and e-mailed the exception
            }

            return default(R);
        }

        private static string HandleError(string action, HttpStatusCode statusCode)
        {
            //if (statusCode == HttpStatusCode.InternalServerError)
            //    return; // Service has already logged and e-mailed the exception

            string message = ((int)statusCode).ToString() + " " + HttpWorkerRequest.GetStatusDescription((int)statusCode);

            var ex = new System.Exception(action + ": " + message);
            try
            {
                throw ex;
            }
            catch (System.Exception)
            {
                ErrorApi.Log(ex);
            }
            return message;
        }

        private static void SetHeaders()
        {
            httpClient.DefaultRequestHeaders.Clear();

            string user = HttpContext.Current.User.Identity.Name.ToUpper();
            user = user.Substring(user.LastIndexOf("\\") + 1);

            if (httpClient.DefaultRequestHeaders.Contains("RacfId"))
                httpClient.DefaultRequestHeaders.Remove("RacfId");
            httpClient.DefaultRequestHeaders.Add("RacfId", user);

            if (httpClient.DefaultRequestHeaders.Contains("Source"))
                httpClient.DefaultRequestHeaders.Remove("Source");
            httpClient.DefaultRequestHeaders.Add("Source", LocalApplication);
        }
    }
}