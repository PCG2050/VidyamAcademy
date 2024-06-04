using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class Subject :INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string Amount { get; set; }
        public List<Video> Videos { get; set; }

        public Color BackgroundColor
        {
            get => AppConstantColor.GetRandomColor();
        }
        public Char FirstCharofName
        {
            get => !string.IsNullOrWhiteSpace(Name) ? Name.ToCharArray()[0] : ' ';
        }

        private bool _isExpanded; 

        public bool IsExpanded
        {
            get => _isExpanded; 
            set
            {
                _isExpanded = value;
                ArrowIcon = IsExpanded ? "uparrow.png" : "downarrow.png";
                OnPropertyChanged("IsExpanded"); 
            }
        }

        private string? _arrowIcon;
        public string ArrowIcon
        {
            get => _arrowIcon ?? "downarrow.png";
            set
            {
                _arrowIcon = value;
                //ArrowIcon = IsExpanded ? "uparrow.png" : "downarrow.png";
                OnPropertyChanged("ArrowIcon");
            }
        }

        public Color ExpandedBackgroundColor
        {
            get => IsExpanded ? Colors.LightGray : Colors.Transparent;
        }



        public event PropertyChangedEventHandler PropertyChanged; 

        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
