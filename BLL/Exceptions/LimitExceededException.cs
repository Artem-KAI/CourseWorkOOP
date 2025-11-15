using System;

namespace BLL.Exceptions
{
    public class LimitExceededException : BusinessException
    {
        public LimitExceededException(string message) : base(message) { }
    }
}
