using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Bases;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Contracts.Extensions;
using Hello6.Domain.Contract.Models.Echo;
using Hello6.Domain.Contract.Models.Extensions;
using Hello6.Domain.SDK.Services.HelloServices.SendCommand;
using Microsoft.Extensions.Logging;

namespace Hello6.Domain.Endpoint.Services.HelloServices.SendCommand
{
    public class HelloSendCommand_Service : DomainServiceBase, IHelloSendCommand_Service
    {
        protected readonly bool _isV2Enabled;
        public HelloSendCommand_Service(ILogger<HelloSendCommand_Service> logger)
            : base(logger)
        {
            if (SdkRuntime.SysConfigs.FeatureToggles.TryGetValue($"{nameof(HelloSendCommand_Service)}_V2_Enabled", out var enabled))
            {
                _isV2Enabled = enabled;
            }
        }

        public async Task<ISvcResponseBaseDto<HelloSendCommand_ResponseDto>> ExecuteAsync(HelloSendCommand_RequestDto requestDto)
        {
            requestDto.PreProcess();

            await Task.CompletedTask;
            return new SvcResponseDto<HelloSendCommand_ResponseDto>
            {
                Data = new HelloSendCommand_ResponseDto
                {
                    Recv = $"Real_ack" + (_isV2Enabled ? "_v2" : "_v1")
                }
            }.Success();
        }
    }
}
