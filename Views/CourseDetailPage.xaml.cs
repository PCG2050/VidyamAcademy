namespace VidyamAcademy.Views
{
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Course selectedCourse, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = new CourseDetailViewModel(selectedCourse, Navigation, apiService);
        }
    }
}