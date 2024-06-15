using Microsoft.Maui.Controls;
using VidyamAcademy.Models;
using VidyamAcademy.Services;
using VidyamAcademy.ViewModels;

namespace VidyamAcademy.Views
{
    public partial class SubjectsPage : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly int _courseId;

        public SubjectsPage(ApiService apiService, int courseId)
        {
            InitializeComponent();
            _apiService = apiService;
            _courseId = courseId;
            BindingContext = new SubjectsPageViewModel(_apiService, _courseId);
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
