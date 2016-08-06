using System.Linq;
using APPS29082016.BLL.Common;
using APPS29082016.BLL.Services.Contract;
using APPS29082016.Core.DTO;
using APPS29082016.DAL.AppsRepository.Contract;
using APPS29082016.DAL.EfRepository;
using APPS29082016.DAL.Response;

namespace APPS29082016.BLL.Services
{
    public class EmployeeServices : CommonServices, IEmployeeService
    {
        private readonly IRepository<tbl_EmployeeInfo> _repository;

        public EmployeeServices(IRepository<tbl_EmployeeInfo> repository)
        {
            _repository = repository;
        }

        public EmployeesInfoList GetEmployees()
        {
            ObjectListResponse<tbl_EmployeeInfo> employeeListResponse = _repository.GetAll();
            if (!employeeListResponse.ExceptionFound && employeeListResponse.StatusCode > 0)
            {
                return new EmployeesInfoList
                {
                    Employees = employeeListResponse.EntityList,
                    EmployeeCount = employeeListResponse.RecordCount
                };
            }
            return new EmployeesInfoList();
        }

        public EmployeeInfo GetEmployeeById(int id)
        {
            ObjectListResponse<tbl_EmployeeInfo> employeeListResponse = _repository.GetEntityByKeyAndValue("Id",
                id.ToString());
            if (!employeeListResponse.ExceptionFound &&
                employeeListResponse.StatusCode > 0 &&
                employeeListResponse.RecordCount == 1)
            {
                var employeeInfo = employeeListResponse.EntityList.FirstOrDefault();
                return new EmployeeInfo
                {
                    Designation = employeeInfo.Designation,
                    Email = employeeInfo.Email,
                    EmpName = employeeInfo.EmpName,
                    Location = employeeInfo.Location,
                    Remarks = employeeInfo.Remarks
                };

            }
            return new EmployeeInfo();
        }

        public EmployeesInfoList SearchEmployee(string searchByFieldName, string keyword)
        {
            if (string.IsNullOrEmpty(searchByFieldName) && string.IsNullOrEmpty(keyword)) return new EmployeesInfoList();
            ObjectListResponse<tbl_EmployeeInfo> searchResult = _repository.GetEntityByKeyAndValue(searchByFieldName,
               keyword);
            if (!searchResult.ExceptionFound &&
                searchResult.StatusCode > 0)
            {
                return new EmployeesInfoList
                {
                    EmployeeCount = searchResult.RecordCount,
                    Employees = searchResult.EntityList
                };
            }
            return new EmployeesInfoList();
        }

        public EmployeesInfoList SearchEmployees(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return new EmployeesInfoList();
            ObjectListResponse<tbl_EmployeeInfo> searchResult = _repository.GetEntityByKeyword(keyword);
            if (!searchResult.ExceptionFound &&
                searchResult.StatusCode > 0)
            {
                return new EmployeesInfoList
                {
                    EmployeeCount = searchResult.RecordCount,
                    Employees = searchResult.EntityList
                };
            }
            return new EmployeesInfoList();
        }

        public OperationResponse CreateEmployee(EmployeeInfo employeesInfo)
        {
            if (!IsDuplicateEntry(_repository, "EmpName", employeesInfo.EmpName)) return new OperationResponse(-4);
            tbl_EmployeeInfo employeeInfo = new tbl_EmployeeInfo
            {
                EmpName = employeesInfo.EmpName,
                Designation = employeesInfo.Designation,
                Email = employeesInfo.Email,
                Location = employeesInfo.Location,
                Remarks = employeesInfo.Remarks
            };
            OperationResponse operationResponse = _repository.AddEntity(employeeInfo);
            return operationResponse;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
