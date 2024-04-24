using System.ComponentModel.DataAnnotations;

namespace QuasarShop.Models;

public class CatalogViewModel
{
    [Display (Name = "Aktif")]
    public bool  Enabled { get; set; }

    [Display (Name = "Katalog Adı")]
    [Required (ErrorMessage ="{0} alanı boş bırakılamaz" )]
    [MaxLength (400,ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
    public   string Name { get; set; }
}

 