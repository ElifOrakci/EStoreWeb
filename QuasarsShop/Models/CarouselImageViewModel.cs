using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace QuasarShop.Models
{
    public class CarouselImageViewModel
    {
        public Guid  Id { get; set; }
       [Display (Name = "Görsel Dosyası")]

        public   IFormFile? ImageFile { get; set; }

        [Display(Name = "Aktif")]
        public bool Enabled { get; set; }

        [Display(Name = "İlk Tarih")]
        [DataType(DataType.Date)]
        public DateTime? DateFirst { get; set; }

        [Display(Name = "Son Tarih")]

        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }

        public string Image { get; set; }

        
    }
}
