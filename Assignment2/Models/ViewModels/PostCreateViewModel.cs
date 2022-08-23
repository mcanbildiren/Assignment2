using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.ViewModels
{
    public class PostCreateViewModel
    {
        [StringLength(50, ErrorMessage = "Başlık en fazla 50 karakterden oluşabilir!")]
        [Required(ErrorMessage = "Başlık boş bırakılamaz!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik boş bırakılamaz!")]
        public string Text { get; set; }

        [StringLength(50, ErrorMessage = "Yazar ismi en fazla 50 karakterden oluşabilir!")]
        [Required(ErrorMessage = "Yazar ismi boş bırakılamaz!")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Kategori boş bırakılamaz!")]
        public int? CategoryId { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
