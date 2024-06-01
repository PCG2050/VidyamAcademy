using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class Course
    {
        public string Name { get; set; }
        public string ImageFileName {  get; set; }

        public string Description { get; set; }

        public string Amount { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}
