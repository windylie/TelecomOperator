using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomOperatorApi.Dtos
{
    public class OperationResponse
    {
        private OperationResponse(bool isSuccessful, string message, object data)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
        }

        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }

        public static OperationResponse Succeed(object data = null)
        {
            return new OperationResponse(true, null, data);
        }

        public static OperationResponse Fail(string message)
        {
            return new OperationResponse(false, message, null);
        }
    }
}
