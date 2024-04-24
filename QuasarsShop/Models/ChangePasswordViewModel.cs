using System.ComponentModel.DataAnnotations;

namespace QuasarShop.Models;

public class ChangePasswordViewModel
{
    [Display(Name = "Şuanki Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "Yeni Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "Yeni Parola Tekrar ")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    [Compare("NewPassword",ErrorMessage = "{0} ve {1} alanı aynı olmalıdır!")]
    public string NewPasswordCheck { get; set; }
}
