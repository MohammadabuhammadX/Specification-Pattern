using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class AttendanceSpecParams 
    {
        public int? EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }

        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Sort { get; set; }  // e.g. "dateAsc" or "dateDesc"
    }
}
