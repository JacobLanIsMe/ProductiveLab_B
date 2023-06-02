using ReproductiveLab_Common.Dtos;
using ReproductiveLab_Common.Dtos.ForOperateSperm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class SpermScoreModel : SpermScoreDto
    {
        public bool isThawed { get; set; }
        public BaseOperateSpermInfoDto? baseSpermInfo_Thaw { get; set; }
        public BaseOperateSpermInfoDto? baseSpermInfo_Fresh { get; set; }
    }
}
