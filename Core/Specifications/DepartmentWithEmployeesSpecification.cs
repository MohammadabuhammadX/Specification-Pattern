using Core.Entities;

namespace Core.Specifications
{
    public class DepartmentWithEmployeesSpecification : BaseSpecification<Department>
    {
        public DepartmentWithEmployeesSpecification(DepartmentSpecParams departmentParams)
            : base(d =>
                (!departmentParams.Id.HasValue || d.Id == departmentParams.Id) &&
                (string.IsNullOrEmpty(departmentParams.Search) || d.Name.ToLower().Contains(departmentParams.Search)))
        {
            AddInclude(d => d.Employees);
            AddOrderBy(d => d.Name);
            ApplyPaging(departmentParams.PageSize * (departmentParams.PageIndex - 1), departmentParams.PageSize);
            if (!string.IsNullOrEmpty(departmentParams.Search))
            {
                switch (departmentParams.Sort)
                {
                    case "nameasc":
                        AddOrderBy(d => d.Name);
                        break;
                    case "namedesc":
                        AddOrderByDesc(d => d.Name);
                        break;
                    default:
                        AddOrderBy(d => d.Name);
                        break;
                }
            }
        }
        public DepartmentWithEmployeesSpecification(int id) : base(d => d.Id == id)
        {
            AddInclude(d => d.Employees);
            AddOrderBy(d => d.Name);
        }
    }
}
