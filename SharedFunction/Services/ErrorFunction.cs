using Reproductive_SharedFunction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Services
{
    public class ErrorFunction : IErrorFunction
    {
        public void ThrowExceptionIfNull<T>(T item, string errorMessage)
        {
            if (item == null)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
