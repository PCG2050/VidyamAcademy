using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace VidyamAcademy.ViewModels
{
    public class CourseDetailViewModel : BaseViewModel
    {
        private readonly INavigation navigation;

        public Course SelectedCourse { get; }
        public Command ViewSubjectsCommand { get; }
        public Command PayNowCommand { get; }

        public CourseDetailViewModel(Course course, INavigation navigation)
        {
            SelectedCourse = course;
            this.navigation = navigation;

            ViewSubjectsCommand = new Command(async () =>
            {
                await navigation.PushAsync(new SubjectsPage(course.Subjects));
            });

            PayNowCommand = new Command(async () => await NavigateToPaymentPage());
        }

        private async Task NavigateToPaymentPage()
        {
            await navigation.PushModalAsync(new PaymentPage(SelectedCourse));
        }

        public string Name => SelectedCourse.Name;
        public string Description => SelectedCourse.Description;
        public string ImageFileName => SelectedCourse.ImageFileName;
        public string Amount => SelectedCourse.Amount;
    }
}
