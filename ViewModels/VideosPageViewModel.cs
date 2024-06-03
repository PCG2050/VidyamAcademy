using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.ViewModels
{

    public partial class VideosPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private List<Video> videos;


        public VideosPageViewModel(Subject selectedSubject)
        {
            if (selectedSubject != null)
            {
                Title = selectedSubject.Name;
                Videos = selectedSubject.Videos;
            }
        }
    }
}


