using System.Threading.Tasks;
using CoreFX.Abstractions.Bases.Interfaces;
using Hello6.Domain.Contract.Models.Echo;

namespace Hello6.Domain.SDK.Services.HelloServices.SendCommand
{
    public interface IHelloSendCommand_Service
    {
        public Task<ISvcResponseBaseDto<HelloSendCommand_ResponseDto>> ExecuteAsync(HelloSendCommand_RequestDto requestDto);
    }
}
