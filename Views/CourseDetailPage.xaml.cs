
using Microsoft.Maui.Controls;

namespace VidyamAcademy.Views
{
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Course selectedCourse)
        {
            InitializeComponent();
            BindingContext = new CourseDetailViewModel(selectedCourse, Navigation);
        }

       
    }

   
}
