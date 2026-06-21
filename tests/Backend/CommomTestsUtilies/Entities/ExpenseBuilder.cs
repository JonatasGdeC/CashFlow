using Bogus;
using CashFlow.Domain.Enitites;
using CashFlow.Domain.Enums;

namespace CommomTestsUtilies.Entities;

public class ExpenseBuilder
{
    public static List<Expense> Collection(User user, uint count = 2)
    {
        List<Expense> list = new();

        if (count == 0)
            count = 1;

        for (int i = 0; i < count; i++)
        {
            Expense expense = Build(user: user);
            list.Add(item: expense);
        }

        return list;
    }

    public static Expense Build(User user)
    {
        return new Faker<Expense>()
            .RuleFor(property: e => e.Id, value: Guid.NewGuid())
            .RuleFor(property: x => x.Title, setter: faker => faker.Commerce.ProductName())
            .RuleFor(property: p => p.Description, setter: faker => faker.Commerce.ProductDescription())
            .RuleFor(property: e => e.Date, setter: faker => faker.Date.Past())
            .RuleFor(property: n => n.Amount, setter: faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(property: s => s.PaymentType, setter: faker => faker.PickRandom<PaymentType>())
            .RuleFor(property: e => e.UserId, setter: _ => user.Id);
    }
}