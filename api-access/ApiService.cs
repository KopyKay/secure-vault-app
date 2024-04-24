using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using vault.Dtos;
using vault.Models;
using Windows.Storage;

namespace api_access
{
    public class ApiService
    {
        private const string BaseUrl = "https://localhost:7028/api";
        private const string TokenEndpoint = "Token";
        private const string UsersEndpoint = "Users";
        private const string CredentialsEndpoint = "Credentials";
        private const string FilesEndpoint = "Files";

        public event EventHandler UnauthorizedRequest;

        private void OnUnauthorizedRequest()
        {
            SetAuthToken(null);
            UnauthorizedRequest?.Invoke(this, EventArgs.Empty);
        }

        private async Task<T> GetAsync<T>(string endpoint) where T : class
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);

            try
            {
                request.AddHeader("Authorization", $"Bearer {GetAuthToken()}");

                var response = await client.ExecuteGetAsync<T>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
            }
            catch (Exception)
            {
                return default(T);
            }

            return default(T);
        }

        private async Task<List<T>> GetCollectionAsync<T>(string endpoint) where T : class
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);

            try
            {
                request.AddHeader("Authorization", $"Bearer {GetAuthToken()}");

                var response = await client.ExecuteGetAsync<List<T>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        private async Task<T> PostAsync<T>(string endpoint, T data) where T : class
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(endpoint);

            try
            {
                if (typeof(T) != typeof(User))
                {
                    request.AddHeader("Authorization", $"Bearer {GetAuthToken()}");
                }

                request.AddJsonBody(data);

                var response = await client.ExecutePostAsync(request);

                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized && typeof(T) != typeof(User))
                {
                    OnUnauthorizedRequest();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        private async Task<bool> DeleteAsync(string endpoint, int id)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"{endpoint}/{id}", Method.Delete);

            try
            {
                request.AddHeader("Authorization", $"Bearer {GetAuthToken()}");
                request.AddUrlSegment("id", id);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return true;
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    OnUnauthorizedRequest();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public void ClearAuthToken() => SetAuthToken(null);

        public string GetAuthToken() => (string)ApplicationData.Current.LocalSettings.Values[TokenEndpoint];

        public void SetAuthToken(string token) => ApplicationData.Current.LocalSettings.Values[TokenEndpoint] = token;

        public async Task RequestAndSetTokenAsync(UserCredentialsDTO userCredentials)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(TokenEndpoint);

            try
            {
                request.AddJsonBody(userCredentials);

                var response = await client.ExecutePostAsync(request);

                if (response.IsSuccessful)
                {
                    if (response.Content != null)
                    {
                        var jsonResponse = JObject.Parse(response.Content);
                        var token = jsonResponse["token"]?.ToString();

                        SetAuthToken(token);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task<User> GetUserAsync() => await GetAsync<User>(UsersEndpoint);

        public async Task<List<File>> GetFilesAsync() => await GetCollectionAsync<File>(FilesEndpoint);

        public async Task<List<Credential>> GetCredentialsAsync() => await GetCollectionAsync<Credential>(CredentialsEndpoint);

        public async Task<User> PostUserAsync(User user) => await PostAsync(UsersEndpoint, user);

        public async Task<File> PostFileAsync(File file) => await PostAsync(FilesEndpoint, file);

        public async Task<Credential> PostCredentialAsync(Credential credential) => await PostAsync(CredentialsEndpoint, credential);

        public async Task<bool> DeleteFileAsync(int id) => await DeleteAsync(FilesEndpoint, id);

        public async Task<bool> DeleteCredentialAsync(int id) => await DeleteAsync(CredentialsEndpoint, id);

        public async Task<bool> DeleteUserAsync(int id) => await DeleteAsync(UsersEndpoint, id);
    }
}
