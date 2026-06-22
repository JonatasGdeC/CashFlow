using CashFlow.Adapter.Exceptions;
using CashFlow.Communication.Response;

namespace CashFlow.App.Pages.Home.Main;

public partial class Home
{
    private readonly ResponseDashboardJson _expenseDashboard = new();
    private readonly ResponseDashboardJson _incomeDashboard = new();
    private bool _isLoading = true;
   
    private List<string> _listFeedbacks = [];
   
    protected override async Task OnInitializedAsync()
    {
        await LoadDashboardAsync();
    }

    private async Task LoadDashboardAsync()
    {
        _isLoading = true;
        _listFeedbacks.Clear();
        await LoadExpenseDashboardAsync();
        await LoadIncomeDashboardAsync();
        _isLoading = false;
    }
    

    private async Task LoadExpenseDashboardAsync()
    {
        try
        {
            ResponseDashboardJson response = await ExpenseApiService.GetDashboard();
            _expenseDashboard.AmountForMonth = response.AmountForMonth;
            _expenseDashboard.TotalAmountForYear = response.TotalAmountForYear;
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks.AddRange(collection: exception.ErrorMessages);
        }
        catch
        {
            _listFeedbacks.Add(item: "Nao foi possivel carregar o dashboard de despesas.");
        }
    }

    private async Task LoadIncomeDashboardAsync()
    {
        try
        {
            ResponseDashboardJson response = await IncomeApiService.GetDashboard();
            _incomeDashboard.AmountForMonth = response.AmountForMonth;
            _incomeDashboard.TotalAmountForYear = response.TotalAmountForYear;
        }
        catch (ApiException exception) when (exception.ErrorMessages.Count > 0)
        {
            _listFeedbacks.AddRange(collection: exception.ErrorMessages);
        }
        catch
        {
            _listFeedbacks.Add(item: "Nao foi possivel carregar o dashboard de rendas.");
        }
    }
}
