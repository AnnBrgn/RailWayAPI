using RailWayAPI.ClassesDTO;
using RailWayAPI.Models;

namespace RailWayAPI.Utils
{
    public static class Utils
    {
        public static UserDTO ToDto(this User user)
        {
            return new UserDTO
            {
                Birthday = user.Birthday,
                Email = user.Email,
                Gender = user.Gender,
                Id = user.Id,
                Lastname = user.Lastname,
                Name = user.Name,
                NumberPassport = user.NumberPassport,
                NumberPhone = user.NumberPhone,
                Patronymic = user.Patronymic,
                SeriesPassport = user.SeriesPassport,
                Login = user.Login,
                Token = user.Token
            };
        }
    }
}
