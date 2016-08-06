﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APPS29082016.DAL.EfRepository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EfRepositoryEntities : DbContext
    {
        public EfRepositoryEntities()
            : base("name=EfRepositoryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_EmployeeInfo> tbl_EmployeeInfo { get; set; }
        public virtual DbSet<tbl_Salary> tbl_Salary { get; set; }
    
        public virtual ObjectResult<GetEmployeeSalary_Result> GetEmployeeSalary(Nullable<int> employeeId)
        {
            var employeeIdParameter = employeeId.HasValue ?
                new ObjectParameter("EmployeeId", employeeId) :
                new ObjectParameter("EmployeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEmployeeSalary_Result>("GetEmployeeSalary", employeeIdParameter);
        }
    }
}
