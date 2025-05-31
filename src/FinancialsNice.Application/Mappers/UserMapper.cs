using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace FinancialsNice.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name!,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                ImgUrl = user.ImgUrl,
                CreatedAt = user.CreatedAt,
                ModifiedAt = user.ModifiedAt,
                Address = user.Address == null ? null : AddressMapper.ToResponse(user.Address),
                Cards = user.Cards == null ? [] : user.Cards.Select(CardMapper.ToResponse).ToList(),
                Type = user.Type,
                Wallet = WalletMapper.ToResponse(user.Wallet),
                Goals = user.Goals == null ? [] : user.Goals.Select(GoalMapper.ToResponse).ToList(),
                Roles =  user.Roles.Select(RoleMapper.ToResponse).ToList()
            };
        }

        public static User ToDomain (UserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                Phone = request.Phone,
                ImgUrl = request.ImgUrl,
                Type = request.Type
            };

            return user;
        }

        public static User ToUpdate (User user, UserUpdate update)
        {
            if (!string.IsNullOrWhiteSpace(update.Name))
                user.Name = update.Name;
            if (!string.IsNullOrWhiteSpace(update.Email))
                user.Email = update.Email;
            if (!update.BirthDate.ToString().IsNullOrEmpty())
                user.BirthDate = update.BirthDate;
            if (!string.IsNullOrWhiteSpace(update.Phone))
                user.Phone = update.Phone;
            if (!string.IsNullOrWhiteSpace(update.ImgUrl))
                user.ImgUrl = update.ImgUrl;
            if (update.Type.HasValue)
                user.Type = update.Type.Value;
            user.ModifiedAt = DateTime.UtcNow;
            
            return user;
        }

        public static UserPreview ToUserPreview(User user)
        {
            return new UserPreview
            {
                Name = user.Name,
                Email = user.Email,
                ImgUrl = user.ImgUrl,
                Type = user.Type,
            };
        }
    }
}