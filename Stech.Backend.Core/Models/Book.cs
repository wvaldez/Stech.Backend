using Stech.Backend.Core;

namespace Stech.Backend.Core
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public int SalesCount { get; set; }
    }
}
