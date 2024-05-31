using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class Subject
    {
        public string Name { get; set; }
        public List<Video> Videos { get; set; }

        public Color BackgroundColor
        {
            get => AppConstantColor.GetRandomColor();
        }
        public Char FirstCharofName
        {
            get => !string.IsNullOrWhiteSpace(Name) ? Name.ToCharArray()[0] : ' ';
        }
    }
}
