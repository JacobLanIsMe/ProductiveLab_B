using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class ResponseDto
    {
        public bool isSuccess { get; set; }
        public string? errorMessage { get; set; }
        public void SetSuccess()
        {
            isSuccess = true;
            errorMessage = null;
        }
        public void SetError(string errorMessage)
        {
            isSuccess = false;
            this.errorMessage = errorMessage;
        }
    }
}
