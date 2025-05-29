using Core.Entities;
using Core.Interface;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> AddPermissionAsync(int roleId, string permission)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);

            if (role == null) 
                throw new KeyNotFoundException();

            var perms = (role.Permissions ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (perms.Contains(permission, StringComparer.OrdinalIgnoreCase))
                throw new InvalidOperationException();

            perms.Add(permission);
            role.Permissions = string.Join(",", perms);
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new ArgumentException($"Role with ID {id} does not exist.");
            }
            await _roleRepository.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<bool> RoleExistsAsync(int id)
        {
            return await _roleRepository.ExistsAsync(id);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }
    }
}

