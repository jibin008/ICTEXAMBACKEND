using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dotnetapi
{
    public class Author
    {
        [Key]
        [JsonIgnore]
        public int aId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

    }

    public class Category
    {
        [Key]
        [JsonIgnore]
        public int cId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class Book
    {
        [Key]
        [JsonIgnore]
        public int bId { get; set; }
        public int aId { get; set; }

        public int cId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }


    }
}
