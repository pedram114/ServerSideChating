using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMessageService
    {
        Task Send(string email, string subject, string message);
    }
}