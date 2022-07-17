using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public class Post:BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public DateTime PostDate { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}