using Microsoft.AspNetCore.Identity;

namespace QuasarShop;

public class AppIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError { Code = base.DuplicateEmail(email).Code, Description = $"{email} zaten kullanımdadır" };

            
    }
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError { Code = base.DuplicateUserName(userName).Code, Description = $"{userName} zaten kullanımdadır" };
    }
}
