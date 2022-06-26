using System;

namespace Shared.DataTransferObjects.ForUpdate
{
    public class PostForUpdateDto
    {
        public Guid CategoryId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
    }
}