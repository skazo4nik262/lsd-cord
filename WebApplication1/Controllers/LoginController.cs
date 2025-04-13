using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("Login")]
        public bool Login(User user)
        {
            using SHA256 hash = SHA256.Create();

            string login = user.Login;
            string password = HashPassword(user.Password);

            if (LsdCorpContext.Instance.Users.FirstOrDefault(s => (s.Login == login) && s.Password == password) == null)
                return false;
            return true;
        }
        public static string HashPassword(string password)
        {
            // Хешируем пароль
            using (var hash = SHA256.Create())
            {
                string hexHash = Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(password)));

                // Определяем количество итераций от 5 до 15
                Random random = new Random();
                int iterations = random.Next(5, 16);

                string result = hexHash;

                for (int i = 0; i < iterations; i++)
                {
                    // Разделяем хэш на две половины
                    int midIndex = result.Length / 2;
                    string firstHalf = result.Substring(0, midIndex);
                    string secondHalf = result.Substring(midIndex);

                    // Делим вторую половину пополам
                    int secondHalfMidIndex = secondHalf.Length / 2;
                    string secondHalfFirstPart = secondHalf.Substring(0, secondHalfMidIndex);
                    string secondHalfSecondPart = secondHalf.Substring(secondHalfMidIndex);

                    // Составляем новую строку
                    result = secondHalfFirstPart + firstHalf + secondHalfSecondPart;
                }

                // Хешируем количество итераций
                string iterationHash = Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(iterations.ToString())));

                // Составляем финальную строку
                result += iterationHash;

                // Возвращаем финальный хэш
                return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(result)));
            }
        }
    }
}
