using FluentValidation;
using Hello6.Domain.Common.Consts;
using Hello6.Domain.Contract.Models.Echo;

namespace Hello6.Domain.Contract.HelloServices.Echo
{
    public class HelloSendCommand_RequestValidator : AbstractValidator<HelloSendCommand_RequestDto>
    {
        public HelloSendCommand_RequestValidator()
        {
            RuleFor(c => c.Send).NotEmpty().Length(HelloConst.EchoCommand.Length, 1000);
        }
    }
}
