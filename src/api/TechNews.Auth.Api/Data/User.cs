using Microsoft.AspNetCore.Identity;

namespace TechNews.Auth.Api.Data;

public sealed class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    public User(Guid id, Guid createdBy, string email, string userName)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        CreatedBy = createdBy;
        Email = email;
        UserName = userName;
    }
}
