using Core.Entities;

namespace Core.Specifications
{

    public class AttendanceWithEmployeeSpecification : BaseSpecification<Attendance>
    {
        public AttendanceWithEmployeeSpecification(AttendanceSpecParams attendanceParams) : base(
            x => (!attendanceParams.EmployeeId.HasValue || x.EmployeeId == attendanceParams.EmployeeId) &&
                 (!attendanceParams.Date.HasValue || x.Date.Date == attendanceParams.Date.Value.Date)


        )
        {
            AddInclude(x => x.Employee);
            AddOrderBy(x => x.Date);
            ApplyPaging((attendanceParams.PageIndex - 1) * attendanceParams.PageSize, attendanceParams.PageSize);
            if (!string.IsNullOrEmpty(attendanceParams.Sort))
            {
                switch (attendanceParams.Sort)
                {
                    case "dateasc":
                        AddOrderBy(a => a.Date);
                        break;
                    case "datedesc":
                        AddOrderByDesc(a => a.Date);
                        break;
                    case "hoursasc":
                        AddOrderBy(a => a.TotalHoursWorked);
                        break;
                    case "hoursdesc":
                        AddOrderByDesc(a => a.TotalHoursWorked);
                        break;
                    default:
                        AddOrderBy(a => a.Date);
                        break;
                }
            }
        }

        public AttendanceWithEmployeeSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Employee);
        }
        public AttendanceWithEmployeeSpecification(int employeeId, bool byEmployeeId)
            : base(x => x.EmployeeId == employeeId)
        {
            AddInclude(x => x.Employee);
            AddOrderBy(x => x.Date);
        }
    }
}
