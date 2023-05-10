﻿using AutoMapper.Configuration.Annotations;

namespace Forum_DAL.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTime WhenReplied { get; set; }

        [Ignore]
        public ICollection<Reply>? Replies { get; set; }

        [Ignore]
        public int NumberOfLikes { get; set; }
    }
}