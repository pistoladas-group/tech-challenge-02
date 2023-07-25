namespace TechNews.Common.Library.Models;

public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}