using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers
{
    public class AddressMapper
    {
        public static AddressResponse ToResponse(Address address)
        {
            return new AddressResponse
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                Neighborhood = address.Neighborhood,
                Complement = address.Complement
            };
        }

        public static Address ToDomain(AddressRequest request)
        {
            return new Address
            {
                Street = request.Street,
                City = request.City,
                Neighborhood = request.Neighborhood,
                State = request.State,
                ZipCode = request.ZipCode,
                Complement = request.Complement
            };
        }
        
        public static Address ToDomainFromUpdate(AddressUpdate request)
        {
            return new Address
            {
                Street = request.Street!,
                City = request.City!,
                Neighborhood = request.Neighborhood!,
                State = request.State!,
                ZipCode = request.ZipCode!,
                Complement = request.Complement
            };
        }

        public static Address ToUpdateFromRequest(Address address, AddressRequest request)
        {
            address.Street = request.Street;
            address.City = request.City;
            address.Neighborhood = request.Neighborhood;
            address.ZipCode = request.ZipCode;
            address.Neighborhood = request.Neighborhood;
            address.Complement = request.Complement;

            return address;
        }

        public static Address ToUpdate(Address address, AddressUpdate update)
        {
            if (update.Street != null && update.Street != address.Street)
                address.Street = update.Street;
            if (update.City != null && update.City != address.City)
                address.City = update.City;
            if (update.Neighborhood != null && update.Neighborhood != address.Neighborhood)
                address.Neighborhood = update.Neighborhood;
            if (update.ZipCode != null && update.ZipCode != address.ZipCode)
                address.ZipCode = update.ZipCode;
            if (update.Neighborhood != null && update.Neighborhood != address.Neighborhood)
                address.Neighborhood = update.Neighborhood;
            if (update.Complement != null && update.Complement != address.Complement)
                address.Complement = update.Complement;
            
            address.ModifiedAt = DateTime.UtcNow;

            return address;
        }
    }
}