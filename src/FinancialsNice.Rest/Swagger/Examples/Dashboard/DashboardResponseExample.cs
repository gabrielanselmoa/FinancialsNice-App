using FinancialsNice.Application.Dtos.Dashboards;
using FinancialsNice.Domain.Design_Pattern;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Dashboard;

public class DashboardResponseExample : IExamplesProvider<ResponseModel<ICollection<TotalAmountByMonth>>>
{
    public ResponseModel<ICollection<TotalAmountByMonth>> GetExamples()
    {
        return new ResponseModel<ICollection<TotalAmountByMonth>>
        {
            Data = new List<TotalAmountByMonth>
            {
                new TotalAmountByMonth
                {
                    Month = "JANUARY",
                    Paid = 300m,
                    Received = 1100m
                },
                new TotalAmountByMonth
                {
                    Month = "APRIL",
                    Paid = 150.75m,
                    Received = 500m
                },
                new TotalAmountByMonth
                {
                    Month = "SEPTEMBER",
                    Paid = 50m,
                    Received = 10m
                }
            },
            Message = "Success",
            Success = true
        };
    }
}