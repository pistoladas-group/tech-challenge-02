namespace TechNews.Web.Models;

public class SignUpViewModel
{
    public Guid Id { get; set; }
    public string? UserName { get; set; } = $"Mock{new Random().Next(0, 1001)}"; // TODO: Lidar com usuário direito
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Repassword { get; set; }
}