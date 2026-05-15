using Bogus;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Cryptography;
using CommomTestsUtilies.Cryptography;

namespace CommomTestsUtilies.Entities;

public  class UserBuilder
{
    public static User Build(Roles role = Roles.User)
    {
        IPasswordEncrypter passwordEncripter = new PasswordEncrypterBuilder().Build();

        Faker<User>? user = new Faker<User>()
            .RuleFor(property: u => u.Id, value: Guid.NewGuid())
            .RuleFor(property: u => u.Name, setter: faker => faker.Person.FirstName)
            .RuleFor(property: u => u.Email, setter: (faker, user) => faker.Internet.Email(firstName: user.Name))
            .RuleFor(property: u => u.Password, setter: (_, user) => passwordEncripter.Encrypt(password: user.Password))
            .RuleFor(property: u => u.Role, setter: _ => role);

        return user;
    }
}