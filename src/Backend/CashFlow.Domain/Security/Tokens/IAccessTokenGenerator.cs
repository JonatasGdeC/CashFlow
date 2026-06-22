using CashFlow.Domain.Enitites;

namespace CashFlow.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}