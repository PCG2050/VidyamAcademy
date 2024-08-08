using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
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
        public void SetAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void ClearAuthHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        public void ClearSessionData()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
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
        


        public async Task<bool> RefreshTokenAsync()
        {
            var refreshToken = await SecureStorage.GetAsync("refresh_token");
           
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }
            var requestBody = new
            {
                RefreshToken = refreshToken
            };
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            
            var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.RefreshToken}", content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(responseBody);

                if (string.IsNullOrEmpty(loginResponse.Token))
                {
                    await SaveTokensAsync(loginResponse);
                    return true;
                }

            }
            return false;
        }
        public async Task<bool> EnsureTokenIsValidAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Check if token is expired and refresh if necessary
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            if (jwtToken.ValidTo <= DateTime.UtcNow)
            {
                return await RefreshTokenAsync();
            }
            return true;
        }

        public bool IsTokenExpired()
        {
            var token = SecureStorage.GetAsync("auth_token").Result;
            if(string.IsNullOrEmpty(token))
            {
                return true;
            }
            var jwtToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        private async Task SaveTokensAsync(LoginResponseDTO loginResponse)
        {
            await SecureStorage.SetAsync("auth_token", loginResponse.Token);
            await SecureStorage.SetAsync("refresh_token", loginResponse.RefreshToken);
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


        public async Task<OrderResult> CreateRazorpayOrderAsync(RazorpayOrderRequestDTO request)
        {
            try
            {
                await AddAuthHeaderAsync();

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/{ApiUrls.CreateOrder}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {errorContent}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                var orderResult = JsonConvert.DeserializeObject<OrderResult>(responseBody);

                return orderResult;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception("An error occurred while creating the Razorpay order.", ex);
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

           
                HttpResponseMessage response = await client.PutAsync($"{_baseUrl}{ApiUrls.UpdateProfile}", content);

                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to update profile: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(string authToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            var response = await _httpClient.GetAsync($"{_baseUrl}{ApiUrls.UserProfile}");

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
        //Videos subjects and courses apis 

        public async Task<List<Course>> GetCourseDetailsAsync()
        {
            try
            {
                await AddAuthHeaderAsync();

               
                var url = $"{_baseUrl}/{ApiUrls.Courses}";
                var response = await _httpClient.GetAsync(url);

               
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Error: Endpoint not found.");
                    return null; 
                }

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var courseResponse = JsonConvert.DeserializeObject<CourseResponse>(responseBody);

                // Ensure each course has non-null Description and Image
                if (courseResponse != null && courseResponse.CourseInfo != null)
                {
                    foreach (var course in courseResponse.CourseInfo)
                    {
                        // Handle null Description and Image if needed
                        if (course.Description == null)
                        {
                            course.Description = "No description available";
                        }
                        if (course.Image == null)
                        {
                            course.Image = "https://techmonitor.ai/wp-content/uploads/sites/4/2017/02/shutterstock_552493561.webp"; 
                        }
                    }
                }

                return courseResponse?.CourseInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public async Task<List<Subject>> GetSubjectsByCourseAsync(int courseId)
        {
            try
            {
                await AddAuthHeaderAsync();
                var url = $"{_baseUrl}SubscriptionCourse/subscriptionSubject-info?courseId={courseId}";
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Error: Endpoint not found.");
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var subjectsResponse = JsonConvert.DeserializeObject<SubjectsResponse>(responseBody);

                return subjectsResponse.SubjectInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Video>> GetVideosBySubjectAsync(int subjectId)
        {
            try
            {
                await AddAuthHeaderAsync();
                var url = $"{_baseUrl}SubscriptionCourse/subscriptionVideo-info?subjectId={subjectId}";
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Error: Endpoint not found.");
                    return new List<Video>();
                }

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var videosResponse = JsonConvert.DeserializeObject<VideosResponse>(responseBody);

                return videosResponse.VideoInfo ?? new List<Video>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching videos: {ex.Message}");
                return new List<Video>();
            }
        }

        public async Task<Subject> GetUserSubjectDetailAsync(int subjectId)
        {
            try
            {
                await AddAuthHeaderAsync();
                var url = $"{_baseUrl}SubscriptionCourse/subscriptionUserSubject-info?subjectId={subjectId}";
                var response = await _httpClient.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                   
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var subjectResponse = JsonConvert.DeserializeObject<SubscriptionUserSubjectInfoResponse>(responseBody);

                // Return the first subject from the list, or null if the list is empty
                return subjectResponse?.UserSubjectInfo?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching subject details: {ex.Message}");
                throw ex;
            }
        }




        public class CourseResponse
        {
            [JsonProperty("courseInfo")]
            public List<Course> CourseInfo { get; set; }
        }
        public class SubjectsResponse
        {
            [JsonProperty("subjectInfo")]
            public List<Subject> SubjectInfo { get; set; }
        }
        public class VideosResponse
        {
            [JsonProperty("videoInfo")]
            public List<Video> VideoInfo { get; set; }
        }
      
        public class SubscriptionUserSubjectInfoResponse
        {
            [JsonProperty("userSubjectInfo")]
            public List<Subject> UserSubjectInfo { get; set; }
        }

    }
}

