using FirstDemo6Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessFriendException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ExceptionEnums.BusinessErrorCode ErrorCode { get; }

        /// <summary>
        /// 
        /// </summary>
        public BusinessFriendException(ExceptionEnums.BusinessErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        public BusinessFriendException(ExceptionEnums.BusinessErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
