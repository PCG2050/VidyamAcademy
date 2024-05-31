namespace VidyamAcademy.Controls;

public partial class FlyoutHeaderControl : StackLayout
{
    private readonly ApiService _apiService;
    private string _oldProfilePicUrl;

    public FlyoutHeaderControl(ApiService apiService)
    {
        InitializeComponent();
        _apiService= apiService;
        LoadUserInfo();
        LoadUserProfile();
    }

    private void LoadUserInfo()
    {
        string username = Preferences.Get("user_name", string.Empty);
        string role = Preferences.Get("user_role", string.Empty);

        lblUserName.Text = username;
        lblUserRole.Text = role;
    }

    private async void LoadUserProfile()
    {
        try
        {
            string authToken = await SecureStorage.GetAsync("auth_token");
            var userProfile = await _apiService.GetUserProfileAsync(authToken);
            if (userProfile != null)
            {
                _oldProfilePicUrl = userProfile.ProfilepicUrl;
                if (!string.IsNullOrEmpty(userProfile.ProfilepicUrl))
                {
                    myImage.Source = ImageSource.FromUri(new Uri(userProfile.ProfilepicUrl));
                }
                else
                {
                    myImage.Source = "profile.png"; // Path to default profile image
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load user profile: {ex.Message}", "OK");
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                using (var stream = await result.OpenReadAsync())
                {
                    string contentType = GetContentType(result.FileName);
                    string newBlobUrl = await _apiService.UploadImageToBlobAsync(result.FileName, stream, contentType);

                    await _apiService.UpdateUserProfileAsync(newBlobUrl);

                    // Immediately update the UI with the new profile picture URL
                    myImage.Source = ImageSource.FromUri(new Uri(newBlobUrl));

                    // Delete the old profile picture from Azure Blob Storage
                    if (!string.IsNullOrEmpty(_oldProfilePicUrl))
                    {
                        await _apiService.DeleteBlobAsync(_oldProfilePicUrl);
                    }

                    // Update the old profile picture URL to the new one
                    _oldProfilePicUrl = newBlobUrl;

                    await Application.Current.MainPage.DisplayAlert("Success", "Profile picture updated successfully!", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => throw new NotSupportedException("File type not supported")
        };
    }
}
