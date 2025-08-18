using Microsoft.AspNetCore.Mvc;

namespace ModelBinding.Models
{
    public class Book
    {
        [FromRoute]
        public int Id { get; set; }
        // This does not work for GET requests - [FromBody]
        public string Name { get; set; }
        public string Author { get; set; }
        public override string ToString()
        {
            return $"'{Name}' by {Author}";
        }
    }
}
