using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Expense.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpensesReadRepository readRepository, ILoggedUser loggedUser) : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Expense>? response = await readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        if (response is null || response.Count == 0)
        {
            return [];
        }

        ExpensesReportExcelGenerator excelGenerator = new(request: request, expenses: response);
        return excelGenerator.Execute();
    }
}