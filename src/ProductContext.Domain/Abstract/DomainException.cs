using System;

namespace ProductContext.Domain.Abstract
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}