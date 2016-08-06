using APPS29082016.Core.DTO;
using APPS29082016.DAL.Response;

namespace APPS29082016.BLL.Services.Contract
{
    public interface IEmployeeService
    {
        EmployeesInfoList GetEmployees();
        EmployeeInfo GetEmployeeById(int id);
        EmployeesInfoList SearchEmployee(string searchByFieldName, string keyword);
        EmployeesInfoList SearchEmployees(string keyword);
        OperationResponse CreateEmployee(EmployeeInfo employeesInfo);
    }
}