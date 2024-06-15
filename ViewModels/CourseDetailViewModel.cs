using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using VidyamAcademy.Services;
using VidyamAcademy.Views;

namespace VidyamAcademy.ViewModels
{
    public class CourseDetailViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly ApiService apiService;

        public Course SelectedCourse { get; }
        public Command ViewSubjectsCommand { get; }
        public Command PayNowCommand { get; }

        public CourseDetailViewModel(Course course, INavigation navigation, ApiService apiService)
        {
            SelectedCourse = course;
            this.navigation = navigation;
            this.apiService = apiService;

            ViewSubjectsCommand = new Command(async () =>
            {
                await navigation.PushAsync(new SubjectsPage(apiService, SelectedCourse.CourseId));
            });

            PayNowCommand = new Command(async () => await navigation.PushModalAsync(new PaymentPage(SelectedCourse, apiService)));
        }

       

        public string Name => SelectedCourse.Name;
        public string? Description => SelectedCourse.Description;
        public string? ImageFileName => SelectedCourse.Image;
        public string? Amount => SelectedCourse.Amount.ToString();
        public string? PaymentStatus => SelectedCourse.PaymentStatus;
        public DateTime? PaidUntil => SelectedCourse.PaidUntil;
        public bool IsPaymentStatusPaid => PaymentStatus == "2"; 
        public bool IsPayNowButtonEnabled => !IsPaymentStatusPaid;


    }
}
