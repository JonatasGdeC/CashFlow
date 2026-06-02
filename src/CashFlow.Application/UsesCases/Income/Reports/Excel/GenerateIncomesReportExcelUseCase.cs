using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Incomes;
using CashFlow.Domain.Services.LoggedUser;

namespace CashFlow.Application.UsesCases.Income.Reports.Excel;

public class GenerateIncomesReportExcelUseCase(IIncomesReadRepository readRepository, ILoggedUser loggedUser) : IGenerateIncomesReportExcelUseCase
{
    public async Task<byte[]> Execute(RequestFilterJson request)
    {
        Domain.Enitites.User currentUser = await loggedUser.Get();
        List<Domain.Enitites.Income>? response = await readRepository.GetFilter(request: request, userId: currentUser.Id);
        
        if (response is null || response.Count == 0)
        {
            return [];
        }

        IncomesReportExcelGenerator excelGenerator = new(request: request, incomes: response);
        return excelGenerator.Execute();
    }
}