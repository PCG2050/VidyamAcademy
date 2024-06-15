using Newtonsoft.Json;

namespace VidyamAcademy.Models
{
    public class Video
    {
        [JsonProperty("videoId")]
        public int VideoId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }

        [JsonProperty("fK_SubjectId")]
        public string Fk_SubjectId { get; set; }

        [JsonProperty("isFree")]
        public bool IsFree { get; set; }

        [JsonProperty("sasToken")]
        public string SasToken { get; set; }

        
     
        public string EffectiveUrl => IsFree || !string.IsNullOrEmpty(SasToken) ? (string.IsNullOrEmpty(SasToken) ? VideoUrl : SasToken) : null;
    }
}
