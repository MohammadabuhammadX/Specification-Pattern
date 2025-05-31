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
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public Attendance()
        {
            EmployeeId = EmployeeId;
            Date = DateTime.Now;
            Status = true; // Default status is true (present)
        }
        public Attendance(int employeeId, DateTime checkInTime, DateTime checkOutTime)
        {
            EmployeeId = employeeId;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Date = DateTime.Now;
            Status = true; // Default status is true (present)
        }
        public void MarkAbsent()
        {
            Status = false; // Mark as absent
            CheckInTime = DateTime.MinValue; // Reset check-in time
            CheckOutTime = DateTime.MinValue; // Reset check-out time
        }
        public void MarkPresent(DateTime checkInTime, DateTime checkOutTime)
        {
            Status = true; // Mark as present
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
        }
        public void SetCheckInTime(DateTime checkInTime)
        {
            if (CheckOutTime != DateTime.MinValue && checkInTime >= CheckOutTime)
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
            Status = newStatus;
            if (!newStatus) // If marking as absent, reset times
            {
                CheckInTime = DateTime.MinValue;
                CheckOutTime = DateTime.MinValue;
            }
        }
    }
}
