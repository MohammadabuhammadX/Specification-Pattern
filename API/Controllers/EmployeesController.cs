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
    }
}
