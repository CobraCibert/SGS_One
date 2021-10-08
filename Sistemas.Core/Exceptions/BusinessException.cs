using System;

namespace Sistemas.Core.Exceptions
{
    public class BusinessException : Exception
    {
        //Constructor
        public BusinessException()
        {

        }
        //Constructor para el mensaje de exceptions
        public BusinessException( string message) : base(message)
        {

        }
    }
}
