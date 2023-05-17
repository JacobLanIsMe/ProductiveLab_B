using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjProductiveLab_B.Dtos.ForTransferIn;
using prjProductiveLab_B.Dtos;
using prjProductiveLab_B.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace prjProductiveLab_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferInController : ControllerBase
    {
        private readonly ITransferInService transferInService;
        public TransferInController(ITransferInService transferInService) 
        {
            this.transferInService = transferInService;
        }
        [HttpPost("AddTransferIn")]
        public BaseResponseDto AddTransferIn(AddTransferInDto input)
        {
            return transferInService.AddTransferIn(input);
        }
    }
}
