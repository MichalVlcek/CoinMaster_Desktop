using System;
using System.Linq;
using System.Threading.Tasks;
using CoinMaster.Exceptions;
using CoinMaster.Model;
using CoinMaster.Utility;

namespace CoinMaster.DB
{
    public class UserRepository
    {
        private readonly Func<CoinDataContext> dataContext;

        public UserRepository(Func<CoinDataContext> dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<User> RegisterUser(string email, string password)
        {
            await using var context = dataContext();

            if (context.Users.Any(u => u.Email == email))
            {
                throw new UserExistsException("User with this email already exists");
            }

            var user = new User()
            {
                Email = email,
                Password = UserUtility.HashPassword(password)
            };
            
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            await using var context = dataContext();

            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new WrongCredentialsException("This user doesn't exist");
            }

            var hashedPassword = UserUtility.HashPassword(password);

            if (user.Password != hashedPassword)
            {
                throw new WrongCredentialsException("Wrong password");
            }

            return user;
        }
    }
}