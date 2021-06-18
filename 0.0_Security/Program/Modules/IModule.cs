using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Security
{
    public interface IModules
    {
        Task<Task> Usage(Message message);
        Task<Task> Ping(Message message);
        Task Mute();
        Task Kick();
        Task Ban();
    }
}