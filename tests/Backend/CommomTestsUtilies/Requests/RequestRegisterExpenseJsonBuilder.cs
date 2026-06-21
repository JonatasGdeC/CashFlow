using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Requests.Expense;

namespace CommomTestsUtilies.Requests;

public static class RequestRegisterExpenseJsonBuilder
{
    public static RequestRegisterExpenseJson Build()
    {
        Faker faker = new();
        
        return new RequestRegisterExpenseJson
        {
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            Date =  faker.Date.Past(),
            Amount = faker.Random.Decimal(min: 1, max: 10000),
            PaymentType = faker.PickRandom<PaymentType>(),
        };
    }
}