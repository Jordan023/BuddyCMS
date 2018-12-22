using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Buddy.WebAPI.Client
{
    [DataContract(Namespace = "")]
    public class LoginResult
    {
        [DataMember(EmitDefaultValue = true, Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(EmitDefaultValue = true, Name = "expires_in")]
        public string ExpiresIn { get; set; }
        [DataMember(EmitDefaultValue = true, Name = "error")]
        public string Error { get; set; }
        [DataMember(EmitDefaultValue = true, Name = "error_description")]
        public string ErrorDescription { get; set; }
    }

    public class Client
    {
        private string _apiUrl = "";

        public string AccessToken
        {
            get; set;
        }

        public Client(string url)
        {
            _apiUrl = url;
        }

        public LoginResult Login(string username, string password, string apikey, string languageCode = "en", string timezone = "utc")
        {
            var loginResult = new LoginResult();

            try
            {
                var data = $"username={username}&password={password}&grant_type=password&apikey={apikey}";

                if (languageCode != null)
                    data += $"&language={languageCode}";
                if (timezone != null)
                    data += $"&timezone={timezone}";

                loginResult = Post<LoginResult>(_apiUrl + "/Token", data);
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    loginResult = JsonConvert.DeserializeObject<LoginResult>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return loginResult;
        }

        public T Post<T>(string url, string data)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            try
            {
                var result = client.UploadString(url, data);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
                //return default(T);
            }
        }

        public string Post(string url, string data)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            client.Headers.Add("Accept", "application/json");

            try
            {
                return client.UploadString(url, data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Get(string url)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };


            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            try
            {
                return client.DownloadString(url);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public T Get<T>(string url)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            try
            {
                var result = client.DownloadString(url);
                dynamic data = JObject.Parse(result);
                return data;
            }
            catch
            {
                return default(T);
            }
        }

        public T Delete<T>(string url)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            try
            {
                var result = client.UploadString(url, "DELETE", "");
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
                //return default(T);
            }
        }

        public string Delete(string url)
        {
            var client = new WebClient()
            {
                Encoding = Encoding.UTF8
            };

            if (!string.IsNullOrEmpty(AccessToken))
            {
                client.Headers.Add("Authorization", "Bearer " + AccessToken);
            }

            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
            client.Headers.Add("Accept", "application/json");

            try
            {
                return client.UploadString(url, "DELETE", "");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
