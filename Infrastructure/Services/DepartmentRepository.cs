using Core.Entities;
using Core.Interface;

namespace Infrastructure.Services
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        public DepartmentRepository(IGenericRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            return await _departmentRepository.AddAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            return await _departmentRepository.UpdateAsync(department);
        }
    }
}
