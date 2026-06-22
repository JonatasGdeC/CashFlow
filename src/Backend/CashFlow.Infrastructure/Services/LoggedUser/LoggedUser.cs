using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Services.LoggedUser;

public class LoggedUser(CashFlowDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    public async Task<User> Get()
    {
        string token = tokenProvider.TokenOnRequest();
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken? jwtSecurityToekn = tokenHandler.ReadJwtToken(token: token);
        string userId = jwtSecurityToekn.Claims.First(predicate: claim => claim.Type == ClaimTypes.Sid).Value;
        
        return await context.Users.AsNoTracking().FirstAsync(predicate: user => user.Id == Guid.Parse(userId));
    }
}