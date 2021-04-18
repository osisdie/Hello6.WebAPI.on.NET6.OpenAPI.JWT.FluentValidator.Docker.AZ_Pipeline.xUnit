using System.Threading.Tasks;
using CoreFX.Abstractions.Bases;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Contracts.Extensions;
using CoreFX.Auth.Contracts.Login;
using CoreFX.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Hello6.Domain.SDK.Services.AuthServices.Login
{
    public class AuthLogin_MockService : DomainServiceBase, IAuthLogin_Service
    {
        public AuthLogin_MockService(ILogger<AuthLogin_MockService> logger)
           : base(logger)
        {

        }

        public async Task<ISvcResponseBaseDto<AuthLogin_ResponseDto>> ExecuteAsync(AuthLogin_RequestDto requestDto)
        {
            await Task.CompletedTask;
            return new SvcResponseDto<AuthLogin_ResponseDto>
            {
                Data = new AuthLogin_ResponseDto
                {
                    UserName = requestDto.Username,
                    UserId = requestDto.Username.ToMD5()
                }
            }.Success();
        }
    }
}
