using APPS29082016.Core.DTO;
using APPS29082016.DAL.Response;

namespace APPS29082016.BLL.Services.Contract
{
    public interface ISalaryService
    {
        OperationResponse CreateSalary(SalaryInfo salaryInfo);
    }
}