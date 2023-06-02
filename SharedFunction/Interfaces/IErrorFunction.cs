using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reproductive_SharedFunction.Interfaces
{
    public interface IErrorFunction
    {
        void ThrowExceptionIfNull<T>(T item, string errorMessage);
    }
}
