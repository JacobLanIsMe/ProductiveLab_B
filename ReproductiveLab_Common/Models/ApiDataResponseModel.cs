using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class ApiDataResponseModel
    {
        public bool IsSuccess { get; set; }
        public object? Data { get; set; }
        public string? ErrorMsg { get; set; }
        public string? ErrorDetail { get; set; }
    }
}
