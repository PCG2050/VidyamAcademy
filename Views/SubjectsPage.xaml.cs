namespace VidyamAcademy.Views;

public partial class SubjectsPage : ContentPage
{

    public SubjectsPage(List<Subject> subjects)
    {
        InitializeComponent();
        subjectsListView.ItemsSource = subjects;
    }

    private async void OnSubjectSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Subject selectedSubject = (Subject)e.SelectedItem;

        try
        {
            // Log the selected subject for debugging
            Console.WriteLine($"Selected subject: {selectedSubject.Name}");

            // Pass the selected subject to the VideosPage constructor
            await Navigation.PushAsync(new VideosPage(selectedSubject));
        }
        catch (Exception ex)
        {
            // Log the detailed exception message
            Console.WriteLine($"Exception: {ex.InnerException?.Message ?? ex.Message}");
            await DisplayAlert("Error", ex.InnerException?.Message ?? ex.Message, "OK");
        }

     ((ListView)sender).SelectedItem = null;
    }
}