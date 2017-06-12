using System.Threading.Tasks;

namespace Darts.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
