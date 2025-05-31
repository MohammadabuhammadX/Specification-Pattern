using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IGenericRepository<Attendance> _attendanceGenericRepository;
        private readonly IAttendaceService _attendaceService;
        private readonly IMapper _mapper;
        public AttendanceController(
            IGenericRepository<Attendance> attendanceGenericRepository,
            IAttendaceService attendaceService,
            IMapper mapper)
        {
            _attendaceService = attendaceService;
            _attendanceGenericRepository = attendanceGenericRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AttendanceDto>> GetAttendanceById(int id)
        {
            var attendance = await _attendaceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound(new ApiResponse(404, "Attendance not found"));
            }

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);
            return Ok(attendanceDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<AttendanceDto>>> GetAllAttendances(
            [FromQuery] AttendanceSpecParams attendanceSpec )
        {

            var attendances = await _attendaceService.GetAllAttendancesWithEmployeesAsync(attendanceSpec);

            var attendanceDtos = _mapper.Map<IReadOnlyList<AttendanceDto>>(attendances);

            return Ok(attendanceDtos);

            //return Ok(attendanceDtos); 
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AttendanceDto>> UpdateAttendance(int id, [FromBody] UpdateAttendanceDto updateAttendanceDto)
        {
            if (updateAttendanceDto == null || id != updateAttendanceDto.Id)
            {
                return BadRequest(new ApiResponse(400, "Invalid attendance data"));
            }
            var existingAttendance = await _attendaceService.GetAttendanceByIdAsync(id);
            if (existingAttendance == null)
            {
                return NotFound(new ApiResponse(404, "Attendance not found"));
            }
            _mapper.Map(updateAttendanceDto, existingAttendance);

            var updatedAttendance = await _attendaceService.UpdateAttendanceAsync(existingAttendance);
            var updatedAttendanceDto = _mapper.Map<AttendanceDto>(updatedAttendance);
            return Ok(updatedAttendanceDto);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var attendance = await _attendaceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound(new ApiResponse(404, "Attendance not found"));
            }
            await _attendaceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
        [HttpPost("restore/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RestoreAttendance(int id)
        {
            var attendance = await _attendaceService.GetByIdIncludingDeletedAsync(id);
            if (attendance == null)
            {
                return NotFound(new ApiResponse(404, "Attendance not found"));
            }
            await _attendaceService.RestoreAttendanceAsync(id);
            return Ok(new ApiResponse(200, "Attendance restored successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AttendanceDto>> CreateAttendance([FromBody] CreateAttendanceDto createAttendanceDto)
        {
            if (createAttendanceDto == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid attendance data"));
            }
            var attendance = _mapper.Map<Attendance>(createAttendanceDto);
            var createdAttendance = await _attendaceService.CreateAttendanceAsync(attendance);
            var createdAttendanceDto = _mapper.Map<AttendanceDto>(createdAttendance);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = createdAttendanceDto.Id }, createdAttendanceDto);
        }
        [HttpPost("checkin/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AttendanceDto>> CheckIn(int employeeId)
        {
            var attendance = await _attendaceService.CheckInAsync(employeeId);

            var attendanceHistory = await _attendaceService.GetByEmployeeAsync(employeeId);

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);
            return Ok(attendanceDto);
        }
        [HttpPost("checkout/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AttendanceDto>> CheckOut(int employeeId)
        {
            var attendance = await _attendaceService.CheckOutAsync(employeeId);
            var attendanceHistory = await _attendaceService.GetByEmployeeAsync(employeeId);

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);
            return Ok(attendanceDto);
        }
    }
}
