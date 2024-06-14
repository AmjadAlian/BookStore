using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class AuthorFormVM
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
    }
}
