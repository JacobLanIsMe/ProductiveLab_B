using ReproductiveLab_Common.Dtos.ForTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Dtos
{
    public class LabMainPageDto
    {
        // 療程天數
        public string? treatmentDay { get; set; }
        // 手術日期時間
        public DateTime surgicalTime { get; set; }
        // 療程 SqlId
        public int courseOfTreatmentSqlId { get; set; }
        // 療程編號
        public string? courseOfTreatmentId { get; set; }
        //客戶病歷號碼
        public int medicalRecordNumber { get; set; }
        // 客戶姓名
        public string? name { get; set; }
        // 主治醫師
        public string? doctor { get; set; }
        // 療程名稱，抓 CourseOfTreatment 資料表的 TreatmentId 欄位
        public TreatmentDto? treatment { get; set; }
        // 療程狀態
        public string? treatmentStatus { get; set; }

        public Guid? spermFromCourseOfTreatmentId { get; set; }
    }
}
