using System;
using System.Collections.Generic;

namespace RuanMou.Projects.Commons.Exceptions
{
    /// <summary>
    /// 框架异常
    /// </summary>
    public class FrameException : Exception
    {
        public string ErrorNo { get; } // 业务异常编号
        public string ErrorInfo { get; }// 业务异常信息

        public IDictionary<string, object> Infos { set; get; } // 业务异常详细信息

        public FrameException(string errorNo, string errorInfo) : base(errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo) : base(errorInfo)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(Exception e)
        {
            ErrorNo = "-1";
            ErrorInfo = e.Message;
        }
    }
}
