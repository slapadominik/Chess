using System.Threading.Tasks;
using Chess.API.Hubs.Models;
using Chess.Logic;

namespace Chess.API.Hubs.Interfaces
{
    public interface ITableClient
    {
        Task ChooseSite(Color color);
        Task UserJoin(UserJoined userJoined);
    }
}