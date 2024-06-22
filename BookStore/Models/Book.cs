using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; } = null!;

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; } = DateTime.Now;

        public string publisher {  get; set; }

        public DateTime PublishDate { get; set; }

        public string? ImgUrl { get; set; }

        public List<BookCategory>? Categories { get; set; } = new List<BookCategory>();
    }
}
