using CoreFX.Auth.Interfaces;
using Hello6.Domain.Endpoint.Controllers.Bases;
using Hello6.Domain.Endpoint.Middlewares;
using Hello6.Domain.SDK.Services.HelloServices.SendCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hello6.Domain.Endpoint.Controllers
{
    [AuthorizeActionFilter]
    [ApiController]
    public partial class HelloController : DomainContollerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IHelloSendCommand_Service _svcSendCommand;
        public HelloController(ILogger<HelloController> logger,
            ISessionAdmin sessionAdmin,
            IHelloSendCommand_Service svcSendCommand,
            IMediator mediator = null) : base(logger, sessionAdmin)
        {
            _mediator = mediator;
            _svcSendCommand = svcSendCommand;
        }
    }
}
