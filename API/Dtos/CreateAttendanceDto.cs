namespace API.Dtos
{
    public class CreateAttendanceDto
    {
        public int EmployeeId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public DateTime Date { get; set; }   
    }

}
