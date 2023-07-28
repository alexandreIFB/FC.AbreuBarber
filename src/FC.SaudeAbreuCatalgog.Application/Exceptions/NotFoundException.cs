using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.AbreuBarber.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message)
        { }

        public static void ThrowIfNull(object? @object, string exceptionMessage)
        {
            if (@object == null)
            {
                throw new NotFoundException(exceptionMessage);
            }
        }
    }
}
