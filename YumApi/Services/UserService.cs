using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;

namespace YumApi.Services
{
    public interface IUserService
    {
        Task<Token> AuthenticateUser(string username, string password);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
    }
    public class UserService : IUserService
    {
        private readonly YumDbContext _context;

        public UserService(YumDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        public async Task<Token> AuthenticateUser(string username, string password)
        {
            var user = _context.User.SingleOrDefault(x => x.Username == username && x.Password == password);

            var role = await _context.Role.FindAsync(user.RoleId);

            // return null if user not found
            if (user == null)
                return null;

            if (role.RoleName != "User")
                return null;

            var appSettings = Configuration.GetSection("AppSettings");

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings["Secret"]);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                  {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.RoleName)
                  }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Token GetUser_Token = new Token
            {
                User_Token = tokenHandler.WriteToken(token),
                User = user
            };


            //return user.WithoutPassword();
            return GetUser_Token;
        }



        //public IEnumerable<User> GetAll()
        //{
        //    return _users.WithoutPasswords();
        //}

        //public User GetById(int id)
        //{
        //    var user = _users.FirstOrDefault(x => x.Id == id);
        //    return user.WithoutPassword();
        //}
    }

}
