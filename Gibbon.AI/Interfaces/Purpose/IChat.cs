using System.Threading.Tasks;
using Gibbon.AI.Models;

namespace Gibbon.AI.Interfaces.Purpose
{
    public interface IChat : IGeneral
    {
        Task<string> SendMessage(string message);
    }
}