using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Interfaces
{
    public interface ISharedFunction
    {
        void ThrowExceptionIfNull<T>(T item, string errorMessage);
    }
}
