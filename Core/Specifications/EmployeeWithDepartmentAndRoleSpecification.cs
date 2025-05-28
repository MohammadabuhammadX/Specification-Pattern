using Core.Entities;

namespace Core.Specifications
{
    public class EmployeeWithDepartmentAndRoleSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentAndRoleSpecification(EmployeeSpecParams employeeParams)
            : base(x =>
            (!employeeParams.DepartmentId.HasValue || x.DepartmentId == employeeParams.DepartmentId) &&
            (!employeeParams.RoleId.HasValue || x.RoleId == employeeParams.RoleId) &&
            (string.IsNullOrEmpty(employeeParams.Search) || x.FirstName.ToLower().Contains(employeeParams.Search))
            )
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Role);
            AddOrderBy(x => x.FirstName);
            ApplyPaging(employeeParams.PageSize * (employeeParams.PageIndex - 1), employeeParams.PageSize);

            if (!string.IsNullOrEmpty(employeeParams.Search))
            {
                switch (employeeParams.Sort)
                {
                    case "nameasc":
                        AddOrderBy(e => e.FirstName);
                        break;
                    case "namedesc":
                        AddOrderByDesc(e => e.FirstName);
                        break;
                    case "lastnameasc":
                        AddOrderBy(e => e.LastName);
                        break;
                    case "lastnamedesc":
                        AddOrderByDesc(e => e.LastName);
                        break;
                    case "salaryasc":
                        AddOrderBy(e => e.Salary);
                        break;
                    case "salarydesc":
                        AddOrderByDesc(e => e.Salary);
                        break;
                    case "hiredateasc":
                        AddOrderBy(e => e.HireDate);
                        break;
                    case "hiredatedesc":
                        AddOrderByDesc(e => e.HireDate);
                        break;
                    default:
                        AddOrderBy(e => e.FirstName);
                        break;
                }
            }

        }

        public EmployeeWithDepartmentAndRoleSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Role);
        }
    }
}
