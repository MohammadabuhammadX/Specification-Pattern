using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IGenericRepository<Employee> employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeToReturnDto>> GetEmployee(int id)
        {
            var spec = new EmployeeWithDepartmentAndRoleSpecification(id);

            var employees = await _employeeRepository.GetEntityWithSpec(spec);

            if (employees == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Employee, EmployeeToReturnDto>(employees);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<EmployeeToReturnDto>>> GetEmployees(
            [FromQuery] EmployeeSpecParams employeeParams)
        {
            var spec = new EmployeeWithDepartmentAndRoleSpecification(employeeParams);
            var employees = await _employeeRepository.ListAsync(spec);

            var results = _mapper.Map<IReadOnlyList<EmployeeToReturnDto>>(employees);

            return Ok(new Pagination<EmployeeToReturnDto>(
                employeeParams.PageIndex,
                employeeParams.PageSize,
                results
            ));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeToReturnDto>> CreateEmployee(
            [FromBody] CreateEmployeeDto createEmployeeDto)
        {

            var employee = _mapper.Map<Employee>(createEmployeeDto);

            var createdEmployee = await _employeeRepository.AddAsync(employee);

            var spec = new EmployeeWithDepartmentAndRoleSpecification(createdEmployee.Id);
            var fullCreatedEmployee = await _employeeRepository.GetEntityWithSpec(spec);

            if (fullCreatedEmployee == null)
            {
                return BadRequest(new ApiResponse(404));
            }
            var result = _mapper.Map<EmployeeToReturnDto>(fullCreatedEmployee);

            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var exits = await _employeeRepository.ExistsAsync(id);
            if (!exits)
            {
                return NotFound(new ApiResponse(404));
            }
            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeToReturnDto>> UpdateEmployee(int id, 
            [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.Id)
            {
                return BadRequest(new ApiResponse(400, "Employee ID mismatch"));
            }
            var exits = await _employeeRepository.ExistsAsync(id);
            if (!exits)
            {
                return NotFound(new ApiResponse(404));
            }
            var employee = _mapper.Map<Employee>(updateEmployeeDto);

            var updatedEmployee = await _employeeRepository.UpdateAsync(employee);

            var spec = new EmployeeWithDepartmentAndRoleSpecification(updatedEmployee.Id);
            var fullUpdatedEmployee = await _employeeRepository.GetEntityWithSpec(spec);
            if (fullUpdatedEmployee == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var result = _mapper.Map<EmployeeToReturnDto>(fullUpdatedEmployee);
            return Ok(result);
        }
    }
}
