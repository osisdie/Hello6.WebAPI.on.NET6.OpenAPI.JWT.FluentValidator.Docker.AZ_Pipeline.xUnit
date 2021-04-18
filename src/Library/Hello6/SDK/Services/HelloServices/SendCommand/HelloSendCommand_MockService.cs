using System.Threading.Tasks;
using CoreFX.Abstractions.Bases;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Contracts.Extensions;
using Hello6.Domain.Contract.Models.Echo;
using Microsoft.Extensions.Logging;

namespace Hello6.Domain.SDK.Services.HelloServices.SendCommand
{
    public class HelloSendCommand_MockService : DomainServiceBase, IHelloSendCommand_Service
    {
        public HelloSendCommand_MockService(ILogger<HelloSendCommand_MockService> logger)
           : base(logger)
        {

        }

        public async Task<ISvcResponseBaseDto<HelloSendCommand_ResponseDto>> ExecuteAsync(HelloSendCommand_RequestDto requestDto)
        {
            await Task.CompletedTask;
            return new SvcResponseDto<HelloSendCommand_ResponseDto>
            {
                Data = new HelloSendCommand_ResponseDto
                {
                    Recv = "Fake_ack"
                }
            }.Success();
        }
    }
}
