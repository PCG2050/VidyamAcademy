namespace VidyamAcademy.Views.Dashboard;

public partial class DashboardPage : ContentPage
{
    

    public DashboardPage(DashboardPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }


    private void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is Course selectedCourse)
            {
                Navigation.PushAsync(new SubjectsPage(selectedCourse.Subjects));
            }
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"Error in OnCourseSelected: {ex.Message}");
        }
        finally
        {
            // Reset the selection
            ((ListView)sender).SelectedItem = null;
        }
    }
}