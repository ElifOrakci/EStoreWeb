using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace QuasarShop.Models;
public class ProductViewModel
{
    [Display(Name = "Aktif")]
    public bool Enabled { get; set; }

    [Display(Name = "Ürün Adı")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [MaxLength(400, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır!")]
    public string Name { get; set; }


    [Display(Name = "Fiyat")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [RegularExpression(@"^[0-9]+(,{1}[0-9]{1,2})?$", ErrorMessage = "Lütfen geçerli bir fiyat yazınız!")]
    public string Price { get; set; }


    [Display(Name = "Açıklama")]
    public string? Description { get; set; }


    [Display(Name = "Görsel")]
    public IFormFile? Image { get; set; }

    [Display(Name = "İndirim Oranı (%)")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Lütfen geçerli bir oran yazınız!")]
    public string DiscountRate { get; set; }

    [Display(Name = "Katalog")]
    public IEnumerable<Guid> Catalogs { get; set; } = new List<Guid>();

    [Display(Name = "Foto Galeri")]
    public IEnumerable<IFormFile>? Images { get; set; }

    public string? OriginalImage { get; set; }
    public List<OriginalImage> OriginalImages { get; set; } = new();

    public string DiscountedPrice => (decimal.Parse(Price) - (int.Parse(DiscountRate) * decimal.Parse(Price) / 100m)).ToString("f2", CultureInfo.CreateSpecificCulture("tr-TR"));

    public List<Guid> ImagesToDelete { get; set; } = new();
}

public class OriginalImage
{
    public Guid Id { get; set; }
    public string Image { get; set; }
}