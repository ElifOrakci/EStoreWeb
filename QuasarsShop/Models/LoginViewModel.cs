using System.ComponentModel.DataAnnotations;

namespace QuasarShop.Models;

public class LoginViewModel
{
    [Display (Name = "E-Posta")]
    [Required (ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType (DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir e-post adresi yazınız!")]
    public string UserName { get; set; }

    [Display(Name = "Parola")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }

}
