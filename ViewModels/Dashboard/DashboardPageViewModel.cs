using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.ViewModels.Dashboard
{
    public class DashboardPageViewModel : BindableObject
    {
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<ImageItem> ImageItems { get; set; }

        public DashboardPageViewModel(ApiService apiService)
        {
            InitializeData();
            InitializeImages();
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl(apiService);
        }

        private void InitializeData()
        {
            // Initialize your data here or retrieve it from a service
          Courses = new ObservableCollection<Course>
          {  new Course
            {
                Name = "IAS COACHING WITH PRELIMINARY AND SECONDARY",
                ImageFileName = "courseimage4.jpg",
                Description = "UPSC Online Coaching is an excellent choice for those aspiring to crack the prestigious UPSC exam. " +
                                "The UPSC Online Coaching Program at Physics Wallah is designed to provide students with the finest and most cost-effective courses. " +
                                "Our expertise and guidance provide students with a solid foundation and a strategic approach to tackle the challenges of the UPSC examination," +
                                " not only via its own courses, but also via OnlyIAS courses. So, start your IAS preparation journey with a Physics Wallah UPSC course now.",
                Amount = "2000",
                Subjects = new List<Subject>
                {
                    new Subject
                    {
                        Name = "Subject A",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 1", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 2", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl= "Thumb2.jpg" },
                            new Video { Title = "Video 3", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 4", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl= "Thumb2.jpg" },
                            new Video { Title = "Video 5", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 6", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample/video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl= "Thumb2.jpg" }

                        }
                    },
                    new Subject
                    {
                        Name = "Subject B",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 3", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 4", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D" ,ThumbnailUrl= "Thumb2.jpg"}
                        }
                    }
                }
            },
            new Course
            {
                Name = "Course 2",
                ImageFileName = "courseimage2.jpg",
                Description = "UPSC Online Coaching is an excellent choice for those aspiring to crack the prestigious UPSC exam. " +
                                "The UPSC Online Coaching Program at Physics Wallah is designed to provide students with the finest and most cost-effective courses. " +
                                "Our expertise and guidance provide students with a solid foundation and a strategic approach to tackle the challenges of the UPSC examination," +
                                " not only via its own courses, but also via OnlyIAS courses. So, start your IAS preparation journey with a Physics Wallah UPSC course now.",
                Amount = "2000",
                Subjects = new List<Subject>
                {
                    new Subject
                    {
                        Name = "Subject C",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 5", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 6", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r" , ThumbnailUrl = "Thumb2.jpg"}
                        }
                    },
                    new Subject
                    {
                        Name = "Subject D",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 7", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r",ThumbnailUrl="Thumb1.jpg" },
                            new Video { Title = "Video 8", VideoUrl = "https://vidyamblob.blob.core.windows.net/videos/sample video2.mp4?sv=2018-03-28&sr=b&sig=BOveCZejI2K0Ll2VZikbtoEAZ9MyNtdVZy7yMRisg0k%3D&se=2024-06-07T05%3A44%3A48Z&sp=r" ,ThumbnailUrl= "Thumb2.jpg"}
                        }
                    }
                }
            },
            new Course
            {
                Name = "Course 3",
                ImageFileName = "courseimage3.jpg",
                Description = "UPSC Online Coaching is an excellent choice for those aspiring to crack the prestigious UPSC exam. " +
                                "The UPSC Online Coaching Program at Physics Wallah is designed to provide students with the finest and most cost-effective courses. " +
                                "Our expertise and guidance provide students with a solid foundation and a strategic approach to tackle the challenges of the UPSC examination," +
                                " not only via its own courses, but also via OnlyIAS courses. So, start your IAS preparation journey with a Physics Wallah UPSC course now.",
                Amount = "2000",
                Subjects = new List<Subject>
                {
                    new Subject
                    {
                        Name = "Subject E",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 9", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D" , ThumbnailUrl = "Thumb1.jpg"},
                            new Video { Title = "Video 10", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D",ThumbnailUrl= "Thumb2.jpg" }
                        }
                    },
                    new Subject
                    {
                        Name = "Subject F",
                        Videos = new List<Video>
                        {
                            new Video { Title = "Video 11", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D" ,ThumbnailUrl="Thumb1.jpg"},
                            new Video { Title = "Video 12", VideoUrl = "https://ramgoldstorage.blob.core.windows.net/videos/video2.mp4?sp=r&st=2024-01-18T07:40:26Z&se=2024-01-25T15:40:26Z&sv=2022-11-02&sr=c&sig=EV8ziD6pgfSYpLVzqrKnPn59d1vmse236nyZ%2F4bUFSM%3D",ThumbnailUrl= "Thumb2.jpg" }
                        }
                    }
                }
            },
            // Add more courses with subjects and videos as needed
          };
        }
        private void InitializeImages()
        {
            ImageItems = new ObservableCollection<ImageItem>
            {
                new ImageItem { ImageSource = "dbimage1.jpg" },
                new ImageItem { ImageSource = "dbimage2.jpg" },
                new ImageItem { ImageSource = "dbimage3.jpg" }
            };
        }
        public class ImageItem
        {
            public string ImageSource { get; set; }
        }

    }
}