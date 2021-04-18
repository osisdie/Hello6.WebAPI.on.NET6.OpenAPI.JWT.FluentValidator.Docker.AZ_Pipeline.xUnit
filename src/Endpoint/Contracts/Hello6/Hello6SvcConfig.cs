using System.ComponentModel.DataAnnotations;

namespace Hello6.Domain.Contract
{
    public class Hello6SvcConfig
    {
        public const string ServiceName = "hello6-api";
        public const string DeployName = "hell6-api";
    }

    public enum Hello6SvcCodeEnum
    {
        [Display(Name = "unset")]
        UnSet = 1000,

        ChangePasswordDenied = 1101,
        ChangePasswordFailed = 1102,

        SocketConnectionFailed = 1201,

        ErrorCode_Max = 2000
    }
}
