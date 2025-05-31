using FinancialsNice.Application.Dtos.Permissions;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class PermissionService(IPermissionRepository permissionRepository) : IPermissionService
{
    public async Task<ResponseModel<ICollection<PermissionResponse>>> GetAllAsync()
    {
        var response = new ResponseModel<ICollection<PermissionResponse>>();
        var permissions = await permissionRepository.GetAllAsync();
        if (!permissions.Any())
            return response.Fail([], "No Permissions were found!");

        var dto = permissions.Select(PermissionMapper.ToResponse).ToList();
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<PermissionResponse?>> GetByIdAsync(int id)
    {
        var response = new ResponseModel<PermissionResponse?>();
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null)
            return response.Fail(null, "Permission not found!");

        var dto = PermissionMapper.ToResponse(permission);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<PermissionResponse?>> GetByNameAsync(PermissionRequest request)
    {
        var response = new ResponseModel<PermissionResponse?>();
        var permission = await permissionRepository.GetByNameAsync(request.Name);
        if (permission == null)
            return response.Fail(null, "Permission not found!");

        var dto = PermissionMapper.ToResponse(permission);
        response.Message = "Success";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<PermissionResponse?>> CreateAsync(PermissionRequest request)
    {
        var response = new ResponseModel<PermissionResponse?>();
        var permission = PermissionMapper.ToDomain(request);
        await permissionRepository.CreateAsync(permission);
        var dto = PermissionMapper.ToResponse(permission);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<PermissionResponse?>> UpdateAsync(int id, PermissionUpdate update)
    {
        var response = new ResponseModel<PermissionResponse?>();
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null)
            return response.Fail(null, "Permission not found!");

        var updated = PermissionMapper.ToUpdate(permission, update);
        await permissionRepository.UpdateAsync(id, updated);
        var dto = PermissionMapper.ToResponse(updated);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(int id)
    {
        var response = new ResponseModel<bool>();
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null)
            return response.Fail(false, "Permission not found!");

        permission.Status = Status.INACTIVE;
        await permissionRepository.SoftDeleteAsync(permission);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(int id)
    {
        var response = new ResponseModel<bool>();
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null)
            return response.Fail(false, "Permission not found!");

        await permissionRepository.HardDeleteAsync(permission);
        return response.Ok(true, "Soft-deleted successfully")!;
    }
}