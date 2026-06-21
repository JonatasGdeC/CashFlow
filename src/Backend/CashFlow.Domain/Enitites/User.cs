using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Enitites;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Roles Role { get; set; } = Roles.User;
}