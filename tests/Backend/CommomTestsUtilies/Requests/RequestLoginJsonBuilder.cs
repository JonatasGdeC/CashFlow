using Bogus;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;

namespace CommomTestsUtilies.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build()
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(property: user => user.Email, setter: faker => faker.Internet.Email())
            .RuleFor(property: user => user.Password, setter: faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}