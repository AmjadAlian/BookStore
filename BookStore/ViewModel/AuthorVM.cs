using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class AuthorVM
    {
        public int Id { get; set; }

        [MaxLength(30,ErrorMessage = "Name must be less than 30 characters ")]
        public string Name { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
    }
}
