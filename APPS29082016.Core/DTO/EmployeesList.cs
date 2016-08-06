using System.Collections.Generic;
using APPS29082016.DAL.EfRepository;

namespace APPS29082016.Core.DTO
{
    public class EmployeesInfoList
    {
        public List<tbl_EmployeeInfo> Employees { get; set; }
        public int EmployeeCount { get; set; }
    }
}
