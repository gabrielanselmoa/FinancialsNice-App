using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class UserService(
    IUserRepository userRepository,
    ISecurityService securityService,
    IAddressRepository addressRepository,
    ICardRepository cardRepository)
    : IUserService
{
    private readonly ICardRepository _cardRepository = cardRepository;
    private readonly ISecurityService _securityService = securityService;

    public async Task<PagedResponseModel<ICollection<UserResponse>>> GetAllAsync(int page, int perPage)
    {
        var response = new PagedResponseModel<ICollection<UserResponse>>();
        var users = await userRepository.GetAllAsync();

        var totalItems = users.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (decimal)perPage);

        if (users.Count() == 0)
        {
            response.Data = null;
            response.Meta = new MetaData()
            {
                Page = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = totalPages,
                NextPage = page < totalPages ? true : null,
                PrevPage = page > 1 ? true : null
            };
            response.Message = "No users were found";
            response.Success = false;
            return response;
        }

        var dto = users.Select(UserMapper.ToResponse).ToList();

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

    public async Task<ResponseModel<UserResponse?>> GetByIdAsync(Guid id)
    {
        var response = new ResponseModel<UserResponse?>();
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return response.Fail(null, "User not found or you don't have permission to view this user.");

        var dto = UserMapper.ToResponse(user);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<UserResponse?>> GetByLoggedUserIdAsync(Guid id)
    {
        var response = new ResponseModel<UserResponse?>();
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return response.Fail(null, "User not found or you don't have permission to view this user.");

        var dto = UserMapper.ToResponse(user);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<UserResponse?>> GetByEmailAsync(string email)
    {
        var response = new ResponseModel<UserResponse?>();

        var user = await userRepository.GetByEmailAsync(email);
        if (user == null)
            return response.Fail(null, "User not found or you don't have permission to view this user.");

        var dto = UserMapper.ToResponse(user);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<UserResponse>> UpdateAsync(Guid userId, UserUpdate update)
    {
        var response = new ResponseModel<UserResponse>();
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            return response.Fail(null, "User not found or you don't have permission to view this user.");

        // Addresses
        Address addressToAssociate;
        if (update.Address != null)
        {
            // Check if updating an existing address that belongs to the user
            if (user.Address?.Id != null && update.Address.Id != null && update.Address.Id == user.Address?.Id)
            {
                var address = await addressRepository.GetByIdAsync(update.Address.Id);
                // Ensure the address being updated actually belongs to this user
                if (address == null || address.OwnerId != userId)
                    return response.Fail(null, "Address not found or does not belong to the user!");

                addressToAssociate = AddressMapper.ToUpdate(address, update.Address);
                user.Address = addressToAssociate;
            }
            else
            {
                // Logic for creating a new address or error if trying to link another's address
                if (user.Address != null && update.Address.Id.HasValue && !user.Address.Id.Equals(update.Address.Id))
                    return response.Fail(null, "User already has an address associated with it. Please remove it before associating another address.");

                if (string.IsNullOrWhiteSpace(update.Address.Street) ||
                    string.IsNullOrWhiteSpace(update.Address.City) ||
                    string.IsNullOrWhiteSpace(update.Address.Neighborhood) ||
                    string.IsNullOrWhiteSpace(update.Address.State) ||
                    string.IsNullOrWhiteSpace(update.Address.ZipCode))
                {
                    return response.Fail(null, "Address is incomplete. Please fill in all fields.");
                }

                var newAddress = AddressMapper.ToDomainFromUpdate(update.Address);
                newAddress.OwnerId = user.Id;
                newAddress.Owner = user;

                await addressRepository.CreateAsync(newAddress);
                addressToAssociate = newAddress;

                user.AddressId = addressToAssociate.Id;
                user.Address = addressToAssociate;
            }
        }

        var updatedUser = UserMapper.ToUpdate(user, update);
        await userRepository.UpdateAsync(userId, updatedUser);

        var dto = UserMapper.ToResponse(updatedUser);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id)
    {
        var response = new ResponseModel<bool>();
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return response.Fail(false, "User not found or you don't have permission to delete this user.");

        user.Status = Status.INACTIVE;
        await userRepository.SoftDeleteAsync(user);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id)
    {
        var response = new ResponseModel<bool>();
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return response.Fail(false, "User not found or you don't have permission to delete this user.");
        
        await userRepository.HardDeleteAsync(user);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}