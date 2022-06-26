using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}