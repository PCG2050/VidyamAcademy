namespace VidyamAcademy.Views
{
    public partial class SubjectsPage : ContentPage
    {
        public SubjectsPage(List<Subject> subjects)
        {
            InitializeComponent();
            BindingContext = new SubjectsPageViewModel(subjects);
        }

        public SubjectsPage()
        {
            InitializeComponent();
            BindingContext = new SubjectsPageViewModel();
        }

        private void OnSubjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var viewModel = (SubjectsPageViewModel)BindingContext;
            var selectedSubject = (Subject)e.SelectedItem;
            viewModel.SubjectSelectedCommand.Execute(selectedSubject);

            // Reset the selected item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
