using Newtonsoft.Json;
using System;

namespace VidyamAcademy.Models
{
    public class Course
    {
        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("paymentStatus")]
        public string? PaymentStatus { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("paidUntil")]
        public DateTime? PaidUntil { get; set; } 
    }
}
