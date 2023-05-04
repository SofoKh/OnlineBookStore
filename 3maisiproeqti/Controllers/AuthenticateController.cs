using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace _3maisiproeqti.Controllers
{
    public class AuthenticateController : BaseController
    {
        private IConfiguration _configuration;
        public AuthenticateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GeneateJsonWebToken(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DFELEERLAUIF3982qef34t6457hyueruw"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("Issuer",
             "Issuer",
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<LoginModel> AuthenticateUser(LoginModel loginModel)
        {
            LoginModel user = null;
            if(loginModel.UserName == "Admin")
            {
                user = new LoginModel
                {
                    UserName = "Admin",
                    Password = "123456"
                };
                
            }
            return user;
        }
        public async Task<IActionResult> LogIn([FromBody]LoginModel loginModel)
        {
            var user = await AuthenticateUser(loginModel);
            if(loginModel == null)
            {
                
            }
            var tokenString = GeneateJsonWebToken(user);
            return Ok();
        }
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync(accessToken);
            return new string[]
            {
                accessToken
            };
        }
        public class LoginModel
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }

        }
    }
}
