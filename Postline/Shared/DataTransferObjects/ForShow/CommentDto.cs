using System;

namespace Shared.DataTransferObjects
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentedDate { get; set; }
    }
}