using CoreFX.Abstractions.Bases.Interfaces;
using CoreFX.Abstractions.Contracts.Extensions;

namespace CoreFX.Abstractions.Contracts
{
    public class SvcResponseDto : ResponseBaseDto, ISvcResponseBaseDto
    {
        public SvcResponseDto() { }
        public SvcResponseDto(bool isSuccess)
        {
            _ = isSuccess ? this.Success() : this.Error();
        }

        public SvcResponseDto(ISvcResponseBaseDto res)
        {
            if (null != res)
            {
                Code = res.Code;
                Msg = res.Msg;
                SubCode = res.SubCode;
                SubMsg = res.SubMsg;
                IsSuccess = res.IsSuccess;
                ExtMap = res.ExtMap;
            }
        }
    }

    /// <summary>
    /// Usage: The generic type could be primitive type or object type, to store actual return data
    ///         Wraper a ResponseBase model so we could give extra information, such as IsSuccess, StatusCode, etc
    /// Example: 
    ///	    SvcResponse<bool>
    ///	    SvcResponse<Account>
    ///	    SvcResponse<Dictionary<string,string>>
    /// </summary>
    public class SvcResponseDto<T> : ResponseBaseDto, ISvcResponseBaseDto<T>
    {
        public SvcResponseDto() { }

        public SvcResponseDto(bool isSuccess)
        {
            _ = isSuccess ? this.Success() : this.Error();
        }

        public SvcResponseDto(bool isSuccess, T data) : this(isSuccess)
        {
            Data = data;
        }

        public SvcResponseDto(ISvcResponseBaseDto res)
        {
            if (null != res)
            {
                Code = res.Code;
                Msg = res.Msg;
                SubCode = res.SubCode;
                SubMsg = res.SubMsg;
                IsSuccess = res.IsSuccess;
                ExtMap = res.ExtMap;
            }
        }

        public T Data { get; set; }
    }

}
