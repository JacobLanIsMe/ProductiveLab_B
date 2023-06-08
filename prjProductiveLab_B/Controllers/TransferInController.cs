using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForTransferIn;
using ReproductiveLab_Service.Interfaces;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferInController : ControllerBase
    {
        private readonly ITransferInService _transferInService;
        public TransferInController(ITransferInService transferInService) 
        {
            _transferInService = transferInService;
        }
        [HttpPost("AddTransferIn")]
        public BaseResponseDto AddTransferIn(AddTransferInDto input)
        {
            return _transferInService.AddTransferIn(input);
        }
    }
}
