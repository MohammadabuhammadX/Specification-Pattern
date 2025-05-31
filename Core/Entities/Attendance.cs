namespace Core.Entities
{
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public TimeSpan TotalHoursWorked
        {
            get
            {
                return CheckOutTime - CheckInTime;
            }
        }
        public bool status { get; set; }
        public DateTime Date { get; set; }
        public Attendance()
        {
            Date = DateTime.Now;
            status = true; // Default status is true (present)
        }
        public Attendance(int employeeId, DateTime checkInTime, DateTime checkOutTime)
        {
            EmployeeId = employeeId;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Date = DateTime.Now;
            status = true; // Default status is true (present)
        }
        public void UpdateAttendance(DateTime checkInTime, DateTime checkOutTime)
        {
            if (checkInTime >= checkOutTime)
                throw new ArgumentException("Check-in time must be earlier than check-out time.", nameof(checkInTime));
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            status = true; // Mark as present after updating times
        }
        public void MarkAbsent()
        {
            status = false; // Mark as absent
            CheckInTime = DateTime.MinValue; // Reset check-in time
            CheckOutTime = DateTime.MinValue; // Reset check-out time
        }
        public void MarkPresent(DateTime checkInTime, DateTime checkOutTime)
        {
            status = true; // Mark as present
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
        }
        public override string ToString()
        {
            return $"Attendance for Employee ID: {EmployeeId}, Date: {Date.ToShortDateString()}, Status: {(status ? "Present" : "Absent")}, Total Hours Worked: {TotalHoursWorked}";
        }
        public override bool Equals(object obj)
        {
            if (obj is Attendance attendance)
            {
                return EmployeeId == attendance.EmployeeId &&
                       CheckInTime == attendance.CheckInTime &&
                       CheckOutTime == attendance.CheckOutTime &&
                       status == attendance.status &&
                       Date.Date == attendance.Date.Date;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(EmployeeId, CheckInTime, CheckOutTime, status, Date.Date);
        }
        public void UpdateStatus(bool newStatus)
        {
            status = newStatus;
            if (!newStatus) // If marking as absent, reset times
            {
                CheckInTime = DateTime.MinValue;
                CheckOutTime = DateTime.MinValue;
            }
        }
        public void UpdateCheckInTime(DateTime newCheckInTime)
        {
            if (newCheckInTime >= CheckOutTime)
                throw new ArgumentException("New check-in time must be earlier than current check-out time.", nameof(newCheckInTime));
            CheckInTime = newCheckInTime;
        }
        public void UpdateCheckOutTime(DateTime newCheckOutTime)
        {
            if (newCheckOutTime <= CheckInTime)
                throw new ArgumentException("New check-out time must be later than current check-in time.", nameof(newCheckOutTime));
            CheckOutTime = newCheckOutTime;
        }
        public void UpdateAttendanceDetails(int employeeId, DateTime checkInTime, DateTime checkOutTime)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive integer.", nameof(employeeId));
            if (checkInTime >= checkOutTime)
                throw new ArgumentException("Check-in time must be earlier than check-out time.", nameof(checkInTime));
            EmployeeId = employeeId;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            status = true; // Mark as present after updating times
        }
        public void ResetAttendance()
        {
            CheckInTime = DateTime.MinValue;
            CheckOutTime = DateTime.MinValue;
            status = false; // Mark as absent
        }
        public void SetAttendanceDate(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
                throw new ArgumentException("Attendance date cannot be in the future.", nameof(date));
            Date = date.Date;
        }
        public void SetEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            Employee = employee;
            EmployeeId = employee.Id; // Ensure EmployeeId is set correctly
        }
        public void SetCheckInTime(DateTime checkInTime)
        {
            if (checkInTime >= CheckOutTime)
                throw new ArgumentException("Check-in time must be earlier than check-out time.", nameof(checkInTime));
            CheckInTime = checkInTime;
        }
        public void SetCheckOutTime(DateTime checkOutTime)
        {
            if (checkOutTime <= CheckInTime)
                throw new ArgumentException("Check-out time must be later than check-in time.", nameof(checkOutTime));
            CheckOutTime = checkOutTime;
        }
        public void SetStatus(bool newStatus)
        {
            status = newStatus;
            if (!newStatus) // If marking as absent, reset times
            {
                CheckInTime = DateTime.MinValue;
                CheckOutTime = DateTime.MinValue;
            }
        }
        public void SetAttendanceDetails(int employeeId, DateTime checkInTime, DateTime checkOutTime, bool newStatus)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive integer.", nameof(employeeId));
            if (checkInTime >= checkOutTime)
                throw new ArgumentException("Check-in time must be earlier than check-out time.", nameof(checkInTime));
            EmployeeId = employeeId;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            status = newStatus; // Set the status based on the provided value
        }
    }
}
