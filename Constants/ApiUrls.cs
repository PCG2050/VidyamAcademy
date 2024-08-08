using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Constants
{
    public static class ApiUrls
    {
        public const string BaseUrl = "https://debugvidyam.azurewebsites.net/api/";

        #region Endpoints

        //LoginPage
        public const string Login = "UserLogin/login";

        public const string RefreshToken = "UserLogin/refresh-token";

        //ForgotPasswordPage
        public const string ForgotPassword = "UserLogin/forgot-password";
        public const string VerifyForgotOTP = "UserLogin/verify-otp";
        public const string VerifyandSetNewPassword = "UserLogin/verify-and-set-new-password";
        
        //UserProfile
        public const string UserProfile = "UserLogin/Profile";
        public const string UpdateProfile = "UserLogin/UpdateProfile";

        //SignUpPage
        public const string SignUp = "Users";
        public const string VerifySignUpOTP = "Users/verify-otp";

        //course
        public const string Courses = "SubscriptionCourse/subscriptionCourse-info";

        //Order
        public const string CreateOrder = "Orders/CreateOrderAsync";

      

        #endregion
    }
}
