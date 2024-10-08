﻿namespace EBev.Domain
{
    public class Blog : BaseEntity
    {
        public string ThumbnailUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
