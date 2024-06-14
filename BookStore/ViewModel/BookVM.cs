using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;


        public string publisher { get; set; }

        public DateTime PublishDate { get; set; } = DateTime.Now;

        public string ImgUrl { get; set; }

        public string Description { get; set; } = null!;

        public List<string> Categories = new List<string>();

    }
}
