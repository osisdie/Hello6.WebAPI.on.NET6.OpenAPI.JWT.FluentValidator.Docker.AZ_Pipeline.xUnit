using CoreFX.Auth.Interfaces;
using Hello6.Domain.Endpoint.Controllers.Bases;
using Hello6.Domain.SDK.Services.AuthServices.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hello6.Domain.Endpoint.Controllers.AuthActions
{
    [ApiController]
    public partial class AuthController : DomainContollerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IAuthLogin_Service _svcLogin;
        public AuthController(ILogger<AuthController> logger,
            ISessionAdmin sessionAdmin,
            IAuthLogin_Service svcLogin,
            IMediator mediator = null) : base(logger, sessionAdmin)
        {
            _mediator = mediator;
            _svcLogin = svcLogin;
        }
    }
}
