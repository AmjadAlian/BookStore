using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
