using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
    }
}
