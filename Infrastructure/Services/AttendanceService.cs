using Core.Entities;
using Core.Interface;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class AttendanceService : IAttendaceService
    {
        private readonly IGenericRepository<Attendance> _attendanceRepository;
        public AttendanceService(IGenericRepository<Attendance> genericRepository)
        {
            _attendanceRepository = genericRepository;
        }

        public async Task<Attendance> CheckInAsync(int employeeId)
        {
            var specParams = new AttendanceSpecParams
            {
                EmployeeId = employeeId,
                Status = true,
                PageSize = 10,
                PageIndex = 1
            };

            var openAttendances = await GetAllAttendancesWithEmployeesAsync(specParams);
            var alreadyCheckedIn = openAttendances.FirstOrDefault(x => x.CheckOutTime == DateTime.MinValue);

            if (alreadyCheckedIn != null)
                throw new InvalidOperationException("Already checked in without checking out.");

            var attendance = new Attendance
            {
                EmployeeId = employeeId,
                CheckOutTime = DateTime.MinValue,
                Date = DateTime.UtcNow.Date
            };
            attendance.SetCheckInTime(DateTime.UtcNow);
            attendance.SetStatus(true);

            var createdAttendance = await _attendanceRepository.AddAsync(attendance);
            return await _attendanceRepository.AddAsync(attendance);
        }
        public async Task<Attendance> CheckOutAsync(int employeeId)
        {
            var spec = new AttendanceWithEmployeeSpecification(new AttendanceSpecParams
            {
                EmployeeId = employeeId
            });

            var attendances = await _attendanceRepository.ListAsync(spec);

            var openAttendance = attendances
                .Where(x => x.CheckOutTime == DateTime.MinValue && x.Date == DateTime.UtcNow.Date)
                .OrderByDescending(x => x.CheckInTime)
                .FirstOrDefault();

            if (openAttendance == null)
                throw new InvalidOperationException("No open check-in found.");

            openAttendance.SetCheckOutTime(DateTime.UtcNow);
            openAttendance.SetStatus(true);

            var updatedAttendance = await _attendanceRepository.UpdateAsync(openAttendance);
            return await GetAttendanceWithEmployeeByIdAsync(updatedAttendance.Id);
        }
        public Task<IReadOnlyList<Attendance>> GetByEmployeeAsync(int employeeId)
        {
            var spec = new AttendanceWithEmployeeSpecification(employeeId, true);
            return _attendanceRepository.ListAsync(spec);
        }


        public async Task<Attendance> CreateAttendanceAsync(Attendance attendance)
        {
            return await _attendanceRepository.AddAsync(attendance);
        }
        public async Task DeleteAttendanceAsync(int id)
        {
            await _attendanceRepository.DeleteAsync(id);
        }
        public async Task<IReadOnlyList<Attendance>> GetAllAttendancesAsync()
        {
            return await _attendanceRepository.GetAllAsync();
        }
        public async Task<Attendance> GetAttendanceByIdAsync(int id)
        {
            return await _attendanceRepository.GetByIdAsync(id);
        }
        public Task<Attendance> GetByIdIncludingDeletedAsync(int id)
        {
            return _attendanceRepository.GetByIdIncludingDeletedAsync(id);
        }
        public async Task RestoreAttendanceAsync(int id)
        {
            await _attendanceRepository.RestoreAsync(id);
        }
        public Task<Attendance> UpdateAttendanceAsync(Attendance attendance)
        {
            return _attendanceRepository.UpdateAsync(attendance);
        }

        public async Task<Attendance> GetAttendanceWithEmployeeByIdAsync(int id)
        {
            var spec = new AttendanceWithEmployeeSpecification(id);
            return await _attendanceRepository.GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Attendance>> GetAllAttendancesWithEmployeesAsync(AttendanceSpecParams specParams)
        {
            var spec = new AttendanceWithEmployeeSpecification(specParams);
            return await _attendanceRepository.ListAsync(spec);
        }

        public async Task GetEntityWithSpec(AttendanceWithEmployeeSpecification spec)
        {
            await _attendanceRepository.GetEntityWithSpec(spec);
        }
    }
}
