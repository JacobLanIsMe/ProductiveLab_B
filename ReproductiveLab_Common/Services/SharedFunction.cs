using ReproductiveLab_Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Services
{
    public  class SharedFunction : ISharedFunction
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
