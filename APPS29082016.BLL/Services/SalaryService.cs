using System.Linq;
using APPS29082016.BLL.Common;
using APPS29082016.BLL.Services.Contract;
using APPS29082016.Core.DTO;
using APPS29082016.DAL.AppsRepository.Contract;
using APPS29082016.DAL.EfRepository;
using APPS29082016.DAL.Response;

namespace APPS29082016.BLL.Services
{
    public class SalaryService:CommonServices, ISalaryService
    {
        private readonly IRepository<tbl_Salary> _salaryRepository;
        private readonly IRepository<tbl_EmployeeInfo> _employeeRepository;

        public SalaryService(IRepository<tbl_Salary> salaryRepository, IRepository<tbl_EmployeeInfo> employeeRepository)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
        }


        public OperationResponse CreateSalary(SalaryInfo salaryInfo)
        {
            if(salaryInfo == null) return new OperationResponse(-4);
            var employee = _employeeRepository.GetEntityByKeyAndValue("EmpName", salaryInfo.SalaryForEmployee).EntityList.FirstOrDefault();
            if (employee != null)
            {
                OperationResponse operationResponse = _salaryRepository.AddEntity(new tbl_Salary
                {
                    EmployeeId =employee.Id,
                    SalaryAmount = salaryInfo.EmployeeSalary.SalaryAmount,
                    ProfessionalTax = salaryInfo.EmployeeSalary.ProfessionalTax
                });
                return operationResponse;
            }
            return new OperationResponse(-87);
        }
    }
}
