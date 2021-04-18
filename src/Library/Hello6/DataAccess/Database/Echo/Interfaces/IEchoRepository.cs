using System.Threading.Tasks;
using CoreFX.Abstractions.Bases.Interfaces;

namespace Hello6.Domain.DataAccess.Database.Echo.Interfaces
{
    public interface IEchoRepository
    {
        Task<ISvcResponseBaseDto> SetVerision(string version);
        Task<ISvcResponseBaseDto<string>> GetVerision();
    }
}
