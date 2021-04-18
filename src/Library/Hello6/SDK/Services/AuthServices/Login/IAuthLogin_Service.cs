using System.Threading.Tasks;
using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Auth.Contracts.Login;

namespace Hello6.Domain.SDK.Services.AuthServices.Login
{
    public interface IAuthLogin_Service
    {
        public Task<ISvcResponseBaseDto<AuthLogin_ResponseDto>> ExecuteAsync(AuthLogin_RequestDto requestDto);
    }
}
