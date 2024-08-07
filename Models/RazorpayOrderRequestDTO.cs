using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidyamAcademy.Models
{
    public class RazorpayOrderRequestDTO
    {
        public long Amount { get; set; }
        public int? CourseId { get; set; }
        public int? SubjectId { get; set; }
        public int UserId { get; set; }
        public string Currency { get; set; }
        public string MobilePhone { get; set; }
    }

    public class OrderResult
    {
        public string OrderId { get; set; }
    }
}
