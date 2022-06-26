using System.Collections.Generic;

namespace Entities.Models
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}