using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Application.UsesCases.Expense.Reports;

public static class TreatPaymentType
{
    public static string Execute(PaymentType paymentType) => paymentType switch
    {
        PaymentType.Cash       => ResourceReportGenerationMessage.CASH,
        PaymentType.CreditCard => ResourceReportGenerationMessage.CREDITCARD,
        PaymentType.DebitCard  => ResourceReportGenerationMessage.DEBITCARD,
        PaymentType.Electronic => ResourceReportGenerationMessage.BANKTRANSFER,
        _                      => throw new ArgumentOutOfRangeException()
    }; 
}