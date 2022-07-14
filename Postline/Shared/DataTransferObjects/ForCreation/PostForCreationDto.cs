using System;

namespace Shared.DataTransferObjects.ForCreation
{
    public class PostForCreationDto
    {
        public Guid CategoryId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
    }
}