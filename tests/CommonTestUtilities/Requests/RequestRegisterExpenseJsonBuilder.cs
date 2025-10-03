using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
  public static RequestExpenseJson Build()
  {
    return new Faker<RequestExpenseJson>()
      .RuleFor(property: r => r.Title, setter: faker => faker.Commerce.Product())
      .RuleFor(property: r => r.Description, setter: faker => faker.Commerce.ProductDescription())
      .RuleFor(property: r => r.Date, setter: faker => faker.Date.Past())
      .RuleFor(property: r => r.Amount, setter: faker => faker.Random.Decimal(min: 1, max: 1000))
      .RuleFor(property: r => r.PaymentType, setter: faker => faker.PickRandom<PaymentType>());
  }
}