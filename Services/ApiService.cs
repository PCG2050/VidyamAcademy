using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Services
{
    public class ApiService
    {
          
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _blobStorageConnectionString;   

        public ApiService(HttpClient httpClient, string baseUrl, string blobStorageConnectionString)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _blobStorageConnectionString = blobStorageConnectionString;
        }
       
        private async Task AddAuthHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }       

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                await AddAuthHeaderAsync();

                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                await AddAuthHeaderAsync();

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                await AddAuthHeaderAsync();

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{endpoint}", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        public async Task DeleteAsync(string endpoint)
        {
            try
            {
                await AddAuthHeaderAsync();

                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{endpoint}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.Login}", content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponseDTO>(responseBody);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }
        public async Task<bool> SignUpAsync(SignUpDTO signupData)
        {
            try
            {
                var json = JsonConvert.SerializeObject(signupData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.SignUp}", content);
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error)
                throw ex;
            }
        }
        public async Task<bool> VerifySignUpOTPAsync(OtpDTO otpVerificationData)
        {
            try
            {
               
                var json = JsonConvert.SerializeObject(otpVerificationData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.VerifySignUpOTP}", content);
                response.EnsureSuccessStatusCode();

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
        }
        public async Task<string> UploadImageToBlobAsync(string fileName, Stream stream, string contentType)
        {
           
            string containerName = "profilepic";


            var blobServiceClient = new BlobServiceClient(_blobStorageConnectionString);


            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            
            var blobClient = containerClient.GetBlobClient(fileName);
            
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });

            return blobClient.Uri.ToString();
        }

        public async Task UpdateUserProfileAsync(string profilepicUrl)
        {
            
            string authToken = await SecureStorage.GetAsync("auth_token");

            using (HttpClient client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var payload = new { profilepicUrl };
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

           
                HttpResponseMessage response = await client.PutAsync($"{ApiUrls.BaseUrl}/{ApiUrls.UpdateProfile}", content);

                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to update profile: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            var response = await _httpClient.GetAsync($"{_baseUrl}/{ApiUrls.UserProfile}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserProfileDTO>(json);
        }
        public async Task DeleteBlobAsync(string blobUrl)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageConnectionString);

            // Extract the container name and blob name from the blob URL
            Uri uri = new Uri(blobUrl);
            string blobName = Path.GetFileName(uri.LocalPath);
            string containerName = "profilepic"; // Assuming the container name is profilepic

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var payload = new { email };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.ForgotPassword}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> VerifyForgotOTPAsync(OtpDTO otpDto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(otpDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.VerifyForgotOTP}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task VerifyAndSetNewPasswordAsync(string email, string otp, string newPassword)
        {
            var payload = new { email, otp, newPassword };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.VerifyandSetNewPassword}", content);
            response.EnsureSuccessStatusCode();
        }

    }
}

