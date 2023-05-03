using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tparf.Dto;
using tparf.Interfaces;
using tparf.Models;
using tparf.Repository;

namespace tparf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(Guid userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpGet("{userId}/product")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult GetProductByUser(Guid userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var userProduct = _mapper.Map<List<ProductDto>>(_userRepository.GetProductByUser(userId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(userProduct);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            var email = _userRepository.GetUsers()
                .Where(c => c.Email.Trim().ToUpper() == userCreate.Email.Trim().ToUpper())
                .FirstOrDefault();

            var phoneNumber = _userRepository.GetUsers()
                .Where(c => c.Phone.Trim().ToUpper() == userCreate.Phone.Trim().ToUpper())
                .FirstOrDefault();

            if (email != null || phoneNumber != null)
            {
                ModelState.AddModelError("", "Такая почта или телефон уже зарегестрированы");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Ошибка при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано!");
        }
    }
}
