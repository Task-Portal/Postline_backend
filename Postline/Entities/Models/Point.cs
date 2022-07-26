namespace Entities.Models
{
    public class Point:BaseEntity
    {
        public bool IsIncrement { get; set; }
        
        public User User { get; set; }
        public Post Post { get; set; }
        
    }
}