using Core.Entities;
using Core.Specifications;

namespace Core.Interface
{
    public interface IAttendaceService
    {
        Task<Attendance> CreateAttendanceAsync(Attendance attendance);
        Task<Attendance> UpdateAttendanceAsync(Attendance attendance);
        Task DeleteAttendanceAsync(int id);
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task<IReadOnlyList<Attendance>> GetAllAttendancesAsync();
        Task RestoreAttendanceAsync(int id);
        Task<Attendance> GetByIdIncludingDeletedAsync(int id);
        Task<Attendance> GetAttendanceWithEmployeeByIdAsync(int id);
        Task<IReadOnlyList<Attendance>> GetAllAttendancesWithEmployeesAsync(AttendanceSpecParams specParams);


        Task<Attendance> CheckInAsync(int employeeId);
        Task<Attendance> CheckOutAsync(int employeeId);
        Task<IReadOnlyList<Attendance>> GetByEmployeeAsync(int employeeId);
        Task GetEntityWithSpec(AttendanceWithEmployeeSpecification spec);
    }
}
