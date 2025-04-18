using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailWayAPI.ClassesDTO;
using RailWayAPI.Models;
using RailWayAPI.Utils;

namespace RailWayAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly RailWaySystemContext _context;

        public UserProfileController(RailWaySystemContext context)
        {
            _context = context;
        }

        [HttpPost("UpdateUserProfileImg")]
        public async Task<IActionResult> UpdateUserImage(UpdateUserProfilePictureDTO profilePictureDTO)
        {
            User user = await _context.Users.FirstOrDefaultAsync(s => s.Token == profilePictureDTO.Token);

            if (user == null)
                return NotFound();

            user.Image = profilePictureDTO.Picture;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("EditUser")]
        public async Task<ActionResult<UserDTO>> EditUser([FromBody] UserDTO userDTO)
        {
            var usr = _context.Users.First(s => s.Token == userDTO.Token);
            usr.NumberPhone = userDTO.NumberPhone;
            usr.Birthday = userDTO.Birthday;
            usr.Name = userDTO.Name;
            usr.Lastname = userDTO.Lastname;
            usr.Patronymic = userDTO.Patronymic;
            usr.Gender = userDTO.Gender;
            usr.Email = userDTO.Email;

            _context.Entry(usr).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(usr.ToDto());
        }
    }
}
