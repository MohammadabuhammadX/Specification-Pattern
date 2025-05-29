using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Department> _departmentRepository;
        public DepartmentController(
            IDepartmentService departmentService,
            IMapper mapper,
            IGenericRepository<Department> departmentRepository
            )
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentAsync();
            var departmentDtos = _mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            if (createDepartmentDto == null)
            {
                return BadRequest(new ApiResponse(400, "Invalid department data"));
            }
            var department = _mapper.Map<Department>(createDepartmentDto);
            var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
            var createdDepartmentDto = _mapper.Map<DepartmentDto>(createdDepartment);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartmentDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            if (updateDepartmentDto == null || id != updateDepartmentDto.Id)
            {
                return BadRequest(new ApiResponse(400, "Invalid department data"));
            }

            var existingDepartment = await _departmentService.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound(new ApiResponse(404, "Department not found"));
            }
            _mapper.Map(updateDepartmentDto, existingDepartment);
            var updatedDepartment = await _departmentService.UpdateDepartmentAsync(existingDepartment);

            var updatedDepartmentDto = _mapper.Map<DepartmentDto>(updatedDepartment);
            return Ok(updatedDepartmentDto);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var existingDepartment = await _departmentService.GetDepartmentByIdAsync(id);
            if (existingDepartment == null)
            {
                return NotFound(new ApiResponse(404));
            }
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
        [HttpGet("with-employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartmentsWithSpec([FromQuery] DepartmentSpecParams specParams)
        {
            var spec = new DepartmentWithEmployeesSpecification(specParams);
            var departments = await _departmentRepository.ListAsync(spec);
            var departmentDtos = _mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

    }
}
