using Bogus;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.User;

namespace CommomTestsUtilies.Requests;

public static class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        Faker faker = new();
        
        return new RequestRegisterUserJson
        {
            Name = faker.Person.FullName,
            Email = faker.Internet.Email(),
            Password = faker.Internet.Password(prefix: "!Aa1"),
        };
    }
}