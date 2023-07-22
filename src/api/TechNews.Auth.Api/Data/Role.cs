using Microsoft.AspNetCore.Identity;

namespace TechNews.Auth.Api.Data;

public sealed class Role : IdentityRole<Guid>
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public Role(Guid id, Guid createdBy)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        CreatedBy = createdBy;
    }
}
