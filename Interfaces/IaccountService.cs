using System.Threading.Tasks;
using ner_pr_api.Dtos.InputDtos;

namespace ner_pr_api.Interfaces
{
    public interface IaccountService
    {
        Task<Account> Login(string username, string password);

        string GenerateToken(Account account);

        Task Register(RegisterRequest account);

        Account GetInfo(string accessToken);

    }
}