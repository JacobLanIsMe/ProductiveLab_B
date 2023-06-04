using ReproductiveLab_Common.Dtos.ForStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReproductiveLab_Common.Models
{
    public class TreatmentSummaryModel
    {
        public Guid ovumDetailId { get; set; }
        public int courseOfTreatmentSqlId { get; set; }
        public string ovumDetailStatus { get; set; }
        public int ovumNumber { get; set; } 
        public DateTime fertilizationTime { get; set; }
        public string? fertilizationMethod { get; set; }
        public Guid observationNoteId { get; set; }
        public BaseStorage? freezeStorageInfo { get; set; }
        public string? ovumSource { get; set; }
        public int ovumFromCourseOfTreatmentSqlId { get; set; }
        public bool hasPickup { get; set; }
        public bool isFreshPickup { get; set; }
        public bool hasFreeze { get; set; }
        public bool hasTransfer { get; set; }
        public bool hasThaw { get; set; }
        public bool isFreezeTransfer { get; set; }
        public bool isTransferThaw { get; set; }
        public bool isFreshTransfer { get; set; }
        public int day_FreshPickup { get; set; }
        public int day_Freeze { get; set; }
        public int day_FreezeTransfer { get; set; }
        public int day_TransferThaw { get; set; }
        public int day_FreshTransfer { get; set; }
        public int day_Thaw { get; set; }
    }
}
