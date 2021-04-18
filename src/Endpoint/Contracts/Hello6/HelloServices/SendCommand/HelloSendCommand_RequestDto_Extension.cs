using System;
using Hello6.Domain.Common.Consts;
using Hello6.Domain.Contract.Models.Echo;

namespace Hello6.Domain.Contract.Models.Extensions
{
    public static class HelloSendCommand_RequestDto_Extension
    {
        public static HelloSendCommand_RequestDto PreProcess(this HelloSendCommand_RequestDto requestDto)
        {
            if (requestDto != null)
            {
#if DEBUG
                if (string.IsNullOrEmpty(requestDto.Send) ||
                    requestDto.Send.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    requestDto.Send = HelloConst.EchoCommand;
                }
#endif
            }

            return requestDto;
        }
    }
}
