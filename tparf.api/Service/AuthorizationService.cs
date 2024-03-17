using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using tparf.api.Data;
using tparf.api.EmailSender;
using tparf.api.Entities;
using tparf.api.Interfaces;
using tparf.dto.Auth;

namespace tparf.api.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly TparfDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        public AuthorizationService(TparfDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        public async Task<Status> ChangePassword(ChangePasswordModel model)
        {
            var status = new Status();
            // check validations
            if (model == null || model.Email == null)
            {
                status.StatusCode = 0;
                status.Message = "please pass all the valid fields";
                return (status);
            }
            // lets find the user
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                status.StatusCode = 0;
                status.Message = "invalid username";
                return (status);
            }
            // check current password
            if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                status.StatusCode = 0;
                status.Message = "invalid current password";
                return (status);
            }

            // change password here
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Failed to change password";
                return (status);
            }
            status.StatusCode = 1;
            status.Message = "Password has changed successfully";
            return status;
        }

        public async Task<Status> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return new Status { StatusCode = 200, Message = "Электронная почта успешно подтверждена" };
                }
            }
            return new Status { StatusCode = 500, Message = "Пользователя с такой электронной почтой не существует" };
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var cartId = await _context.Carts.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstOrDefaultAsync();

                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Email == user.Email);
                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Email = user.Email,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.UtcNow.AddDays(1)
                    };
                    _context.TokenInfo.Add(info);
                }

                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return null;
                }
                return (new LoginResponse
                {
                    Email = user.Email,
                    CartId = cartId,
                    Id = user.Id,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    StatusCode = 200,
                    Message = "Вход успешен"
                });

            }
            //login failed condition

            return (
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        public async Task<LoginResponse> Login(string email, string pass)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, pass))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var cartId = await _context.Carts.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstOrDefaultAsync();

                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Email == user.Email);
                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Email = user.Email,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.UtcNow.AddDays(1)
                    };
                    _context.TokenInfo.Add(info);
                }

                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return null;
                }
                return (new LoginResponse
                {
                    Email = user.Email,
                    CartId = cartId,
                    Id = user.Id,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    StatusCode = 200,
                    Message = "Вход успешен"
                });

            }
            //login failed condition

            return (
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        public async Task<Status> Registration(RegistrationModel model)
        {
            var status = new Status();
            if (model == null)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return (status);
            }
            // check if user exists
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Пользователь с такой электронной почтой уже существует";
                return (status);
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyName = model.CompanyName,
                PhoneNumber = model.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
                Name = model.Email
            };
            // create a user here
            var result = await _userManager.CreateAsync(user, model.Password);

            Cart cart = new Cart
            {
                UserId = user.Id,
            };

            await _context.Carts.AddAsync(cart);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return (status);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            // Добавляю токен для подтверждения электронной почты

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            status.StatusCode = 200;
            status.Message = token;

            return (status);
        }
    }
}
