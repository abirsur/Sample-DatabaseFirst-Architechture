using System.Web.Mvc;
using APPS29082016.BLL.Services.Contract;
using APPS29082016.Core.DTO;
using APPS29082016.DAL.EfRepository;
using APPS29082016.DAL.Response;

namespace APPS29082016.Controllers
{
    public class EmployeeController:Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryService _salaryService;

        public EmployeeController(IEmployeeService employeeService, ISalaryService salaryService)
        {
            this._employeeService = employeeService;
            _salaryService = salaryService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            EmployeesView();
            return View("~/Views/Employee/Index.cshtml");
        }

        public PartialViewResult EmployeesView()
        {
            EmployeesInfoList employeesInfoLists = _employeeService.GetEmployees();
            return PartialView("~/Views/Employee/PartialViews/_employeeView.cshtml", employeesInfoLists);
        }

        public PartialViewResult CreateView()
        {
            return PartialView("~/Views/Employee/PartialViews/_employeeCreate.cshtml");
        }

        [HttpGet]
        public ActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(1);
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(EmployeeInfo employeeInfo)
        {
           OperationResponse operationResponse = _employeeService.CreateEmployee(employeeInfo);
            SalaryInfo salaryInfo = new SalaryInfo
            {
                EmployeeSalary = new tbl_Salary {SalaryAmount = (decimal) 18000.00, ProfessionalTax = (decimal) 678.00},
                SalaryForEmployee = "Sona"
            };
            if (!operationResponse.ExceptionFound && operationResponse.StatusCode > 0)
            {
                operationResponse =
                    _salaryService.CreateSalary(salaryInfo);
            }
            return RedirectToAction("Index");
        }
    }
}