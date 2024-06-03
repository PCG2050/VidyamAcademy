namespace VidyamAcademy.ViewModels
{
    public class CourseDetailViewModel
    {
        private readonly INavigation navigation;

        public Course SelectedCourse { get; }
        public Command ViewSubjectsCommand { get; }
        public Command PayNowCommand { get; }

        public CourseDetailViewModel(Course course, INavigation navigation)
        {
            SelectedCourse = course;

            ViewSubjectsCommand = new Command(async () =>
            {


                await navigation.PushAsync(new SubjectsPage(course.Subjects));
            });

            PayNowCommand = new Command(() =>
            {
                // Implement payment logic here
            });
        }

        public string Name => SelectedCourse.Name;
        public string Description => SelectedCourse.Description;
        public string ImageFileName => SelectedCourse.ImageFileName;
        public string Amount => SelectedCourse.Amount;
    }
}
