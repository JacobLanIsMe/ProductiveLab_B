using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class BaseResponseDto
    {
        public bool isSuccess { get; set; }
        //public string? successMessage { get; set; } = null;
        public string? errorMessage { get; set; }
        public void SetSuccess()
        {
            this.isSuccess = true;
            this.errorMessage = null;
        }
        public void SetError(string error)
        {
            this.isSuccess = false;
            this.errorMessage = error;
        }
    }
}
