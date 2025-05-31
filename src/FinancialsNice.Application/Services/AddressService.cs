using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;

    public AddressService(IAddressRepository addressRepository, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
    }

    public async Task<PagedResponseModel<ICollection<AddressResponse>>> GetAllAsync(int page, int perPage, Guid userId)
    {
        var response = new PagedResponseModel<ICollection<AddressResponse>>();
        var addresses = await _addressRepository.GetAllAsync(userId);
        if (!addresses.Any())
        {
            response.Data = [];
            response.Meta = new MetaData()
            {
                TotalItems = 0,
                TotalPages = 0,
                Page = page,
                PerPage = perPage
            };
            response.Message = "No addresses were found";
            response.Success = true;
            return response;
        };
        var dto = addresses.Select(AddressMapper.ToResponse).ToList();

        var totalItems = addresses.Count;
        var totalPages = (int)Math.Ceiling((double)totalItems / perPage);

        response.Data = dto;
        response.Meta = new MetaData()
        {
            Page = page,
            PerPage = perPage,
            TotalItems = totalItems,
            TotalPages = totalPages,
            NextPage = page < totalPages ? true : null,
            PrevPage = page > 1 ? true : null
        };
        response.Message = "Success";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<AddressResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<AddressResponse?>();
        var address = await _addressRepository.GetByIdAsync(id);

        if (address == null || address.OwnerId != userId)
            return response.Fail(null, "Address not found or does not belong to the user!");

        var dto = AddressMapper.ToResponse(address);
        return response.Ok(dto, "Address retrieved successfully.");
    }

    public async Task<ResponseModel<AddressResponse?>> CreateAsync(AddressRequest request, Guid userId)
    {
        var response = new ResponseModel<AddressResponse?>();
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            return response.Fail(null, "Owner not found!");

        var address = AddressMapper.ToDomain(request);
        address.OwnerId = userId;

        await _addressRepository.CreateAsync(address);
        var dto = AddressMapper.ToResponse(address);
        return response.Ok(dto, "Address created successfully.");
    }

    public async Task<ResponseModel<AddressResponse?>> UpdateAsync(Guid id, Guid userId, AddressUpdate update)
    {
        var response = new ResponseModel<AddressResponse?>();
        var existingAddress = await _addressRepository.GetByIdAsync(id);
        
        if (existingAddress == null || existingAddress.OwnerId != userId)
            return response.Fail(null, "Address not found or does not belong to the user!");

        var updatedAddress = AddressMapper.ToUpdate(existingAddress, update);
        await _addressRepository.UpdateAsync(id, updatedAddress);
        
        var dto = AddressMapper.ToResponse(updatedAddress);
        return response.Ok(dto, "Address updated successfully.");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var address = await _addressRepository.GetByIdAsync(id);
        
        if (address == null || address.OwnerId != userId)
            return response.Fail(false, "Address not found or does not belong to the user!");

        address.Status = Status.INACTIVE;
        await _addressRepository.SoftDeleteAsync(address);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var address = await _addressRepository.GetByIdAsync(id);
        
        if (address == null || address.OwnerId != userId)
            return response.Fail(false, "Address not found or does not belong to the user!");

        await _addressRepository.HardDeleteAsync(address);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}