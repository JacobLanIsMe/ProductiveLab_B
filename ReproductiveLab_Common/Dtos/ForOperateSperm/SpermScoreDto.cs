using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos.ForOperateSperm
{
    public class SpermScoreDto
    {
        public decimal volume { get; set; }
        public decimal concentration { get; set; }
        public decimal activityA { get; set; }
        public decimal activityB { get; set; }
        public decimal activityC { get; set; }
        public decimal activityD { get; set; }
        public decimal? morphology { get; set; }
        public int? abstinence { get; set; }
        public int spermScoreTimePointId { get; set; }
        public string? spermScoreTimePoint { get; set; }
        public DateTime recordTime { get; set; }
        public Guid embryologist { get; set; }
        public string? embryologistName { get; set; }
        public Guid courseOfTreatmentId { get; set; }
        public int courseOfTreatmentSqlId { get; set; }
        public BaseOperateSpermInfoDto? baseSpermInfo { get; set; }
    }
}
