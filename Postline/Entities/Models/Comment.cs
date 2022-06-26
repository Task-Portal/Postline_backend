using System;

namespace Entities.Models
{
    public class Comment:BaseEntity
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CommentedDate { get; set; }
        public string Text { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
    }
}