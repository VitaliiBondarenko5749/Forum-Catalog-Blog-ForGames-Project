using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_BAL.DTO
{
    public class CommentInfoDTO
    {
        public string? Content { get; set; }
        public string? WhenReplied { get; set; }
        public int NumberOfLikes { get; set; }
        public ICollection<ReplyInfoDTO>? Replies { get; set; }
    }
}
