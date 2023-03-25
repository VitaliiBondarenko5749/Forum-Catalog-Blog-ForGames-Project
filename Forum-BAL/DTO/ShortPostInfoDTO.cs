using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Forum_BAL.DTO
{
    public class ShortPostInfoDTO
    {
        public string? Title { get; set; }
        public string? CreatedAt { get; set; }
        public ICollection<ShortGameInfoDTO>? Games { get; set; }
    }
}
