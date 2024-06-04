using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VidyamAcademy.ViewModels
{

    public partial class VideosPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private List<Video> videos;

        public Subject SelectedSubject { get; }

        public VideosPageViewModel(Subject selectedSubject)
        {
            if (selectedSubject != null)
            {
                Title = selectedSubject.Name;
                Videos = selectedSubject.Videos;
                SelectedSubject = selectedSubject;
            }
        }
        public ICommand PayNowCommand => new Command(async () =>
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SubjectPaymentPage(SelectedSubject));
        });
    }
}


