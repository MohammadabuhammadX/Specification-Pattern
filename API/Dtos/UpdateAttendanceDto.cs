namespace API.Dtos
{
    public class UpdateAttendanceDto
    {
        public int Id { get; set; }   // must match URL id
        public DateTime? CheckInTime { get; set; }  
        public DateTime? CheckOutTime { get; set; }
        public bool? Status { get; set; }  
        public DateTime? Date { get; set; }
    }

}
