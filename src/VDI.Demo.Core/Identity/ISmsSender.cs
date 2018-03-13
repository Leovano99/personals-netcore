using System.Threading.Tasks;

namespace VDI.Demo.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}