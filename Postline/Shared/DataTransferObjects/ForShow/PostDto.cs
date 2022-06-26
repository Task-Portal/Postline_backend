using System;

namespace Shared.DataTransferObjects.ForShow
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public DateTime PostDate { get; set; }
        public int Rating { get; set; }

        public PostDto()
        {
            
        }
    }
}