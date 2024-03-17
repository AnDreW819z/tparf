using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tparf.api.Data;
using tparf.api.EmailSender;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;

namespace tparf.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _service;
        private readonly IEmailService _emailService;
        public AuthorizationController(IAuthorizationService service, IEmailService emailService)
        {
            _service = service;
            _emailService= emailService;
        }
        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _service.ChangePassword(model);
                if (result != null) 
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _service.Login(model);
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _service.Registration(model);
                    if (response.StatusCode == 200)
                    {
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authorization", new { response.Message, email = model.Email }, Request.Scheme);
                        var message = new Message(new string[] { model.Email }, $"Подтвердите адрес электронной почты на tparf.ru", $"{model.FirstName}, Вы были зарегистрированы на портале ТОРГОВО-ПРОМЫШЛЕННОЕ АГЕНТСТВО, для дальнейшего сотрудничества пожалуйста подтвердите адресс электронной почты: {confirmationLink}");
                        await _emailService.SendEmail(message);
                        var login = await _service.Login(model.Email, model.Password);
                        return Ok(login);
                    }
                    return Ok(response);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var response = await _service.ConfirmEmail(token, email);
                if (response != null)
                    return Ok(response);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка получения данных из базы данных");
            }
        }
    }
}
