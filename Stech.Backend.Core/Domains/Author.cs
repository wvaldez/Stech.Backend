using Stech.Backend.Core;
using System;

namespace Stech.Backend.Core
{
    public class Author:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool IsActive { get; set; }        
    }
}
