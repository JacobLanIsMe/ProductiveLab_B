using System;
using System.Collections.Generic;

namespace ReproductiveLabDB.Models
{
    public partial class CourseOfTreatment
    {
        public CourseOfTreatment()
        {
            OvumDetailCourseOfTreatments = new HashSet<OvumDetail>();
            OvumDetailOvumFromCourseOfTreatments = new HashSet<OvumDetail>();
            SpermFreezes = new HashSet<SpermFreeze>();
            SpermScores = new HashSet<SpermScore>();
            SpermThaws = new HashSet<SpermThaw>();
        }

        public int SqlId { get; set; }
        public Guid CourseOfTreatmentId { get; set; }
        public Guid Doctor { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime SurgicalTime { get; set; }
        public int TreatmentStatusId { get; set; }
        public string? Memo { get; set; }
        public int? OvumSituationId { get; set; }
        public int? OvumSourceId { get; set; }
        public int? OvumOperationId { get; set; }
        public int? SpermSituationId { get; set; }
        public int? SpermSourceId { get; set; }
        public int? SpermOperationId { get; set; }
        public int? SpermRetrievalMethodId { get; set; }
        public int? EmbryoSituationId { get; set; }
        public int? EmbryoOperationId { get; set; }
        public bool IsTerminated { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee DoctorNavigation { get; set; } = null!;
        public virtual GermCellOperation? EmbryoOperation { get; set; }
        public virtual GermCellSituation? EmbryoSituation { get; set; }
        public virtual GermCellOperation? OvumOperation { get; set; }
        public virtual GermCellSituation? OvumSituation { get; set; }
        public virtual GermCellSource? OvumSource { get; set; }
        public virtual GermCellOperation? SpermOperation { get; set; }
        public virtual SpermRetrievalMethod? SpermRetrievalMethod { get; set; }
        public virtual GermCellSituation? SpermSituation { get; set; }
        public virtual GermCellSource? SpermSource { get; set; }
        public virtual TreatmentStatus TreatmentStatus { get; set; } = null!;
        public virtual ICollection<OvumDetail> OvumDetailCourseOfTreatments { get; set; }
        public virtual ICollection<OvumDetail> OvumDetailOvumFromCourseOfTreatments { get; set; }
        public virtual ICollection<SpermFreeze> SpermFreezes { get; set; }
        public virtual ICollection<SpermScore> SpermScores { get; set; }
        public virtual ICollection<SpermThaw> SpermThaws { get; set; }
    }
}
