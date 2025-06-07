using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async Task<ResponseModel<ICollection<RoleResponse>>> GetAllAsync()
    {
        var response = new ResponseModel<ICollection<RoleResponse>>();
        var roles = await roleRepository.GetAllAsync();
        if (!roles.Any()) 
            return response.Fail([], "No Roles were found!");

        var dto = roles.Select(RoleMapper.ToResponse).ToList();
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<RoleResponse?>> GetByIdAsync(int id)
    {
        var response = new ResponseModel<RoleResponse?>();
        var role = await roleRepository.GetByIdAsync(id);
        if (role == null)
            return response.Fail(null, "Role not found!");

        var dto = RoleMapper.ToResponse(role);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<RoleResponse?>> GetByNameAsync(RoleRequest request)
    {
        var response = new ResponseModel<RoleResponse?>();
        var role = await roleRepository.GetByNameAsync(request.Name);
        if (role == null)
            return response.Fail(null, "Role not found!");

        var dto = RoleMapper.ToResponse(role);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<RoleResponse?>> CreateAsync(RoleRequest request)
    {
        var response = new ResponseModel<RoleResponse?>();
        var role = await roleRepository.GetByNameAsync(request.Name);

        if (role != null) 
            return response.Fail(null, "Role already exists!");

        var roleDomain = RoleMapper.ToDomain(request);
        await roleRepository.CreateAsync(roleDomain);
        var dto = RoleMapper.ToResponse(roleDomain);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<RoleResponse?>> UpdateAsync(int id, RoleUpdate update)
    {
        var response = new ResponseModel<RoleResponse?>();
        var role = await roleRepository.GetByIdAsync(id);
        if (role == null)
            return response.Fail(null, "Role not found!");

        var updated = RoleMapper.ToUpdate(role, update);
        await roleRepository.UpdateAsync(id, updated);
        var dto = RoleMapper.ToResponse(updated);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(int id)
    {
        var response = new ResponseModel<bool>();
        var role = await roleRepository.GetByIdAsync(id);
        if (role == null)
            return response.Fail(false, "Role not found!");

        role.Status = Status.INACTIVE;
        await roleRepository.SoftDeleteAsync(role);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(int id)
    {
        var response = new ResponseModel<bool>();
        var role = await roleRepository.GetByIdAsync(id);
        if (role == null)
            return response.Fail(false, "Role not found!");
        
        await roleRepository.HardDeleteAsync(role);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}