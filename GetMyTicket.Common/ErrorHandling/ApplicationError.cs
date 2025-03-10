using System.Net;

namespace GetMyTicket.Common.ErrorHandling
{
    public class ApplicationError : Exception
    {
       
        public override string Message { get; }

        public ApplicationError( string message)
        {
            Message = message;
        }
    }
}
