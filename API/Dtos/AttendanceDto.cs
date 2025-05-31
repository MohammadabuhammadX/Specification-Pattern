namespace API.Dtos
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } // optional if you join Employee
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public TimeSpan TotalHoursWorked { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
