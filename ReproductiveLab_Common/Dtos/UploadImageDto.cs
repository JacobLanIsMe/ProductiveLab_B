using Imgur.API.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class UploadImageDto
    {
        [Required]
        public string? IFormFileString { get; set; }
        [Required]
        public ApiClient? ApiClient { get; set; }
    }
}
